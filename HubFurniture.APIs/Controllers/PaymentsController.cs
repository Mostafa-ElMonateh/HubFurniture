using System.Security.Claims;
using AutoMapper;
using HubFurniture.APIs.Dtos;
using HubFurniture.APIs.Errors;
using HubFurniture.Core.Contracts;
using HubFurniture.Core.Contracts.Contracts.Repositories;
using HubFurniture.Core.Contracts.Contracts.Services;
using HubFurniture.Core.Entities;
using HubFurniture.Core.Entities.Order_Aggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace HubFurniture.APIs.Controllers
{
    
    public class PaymentsController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly IUnitOfWork _unitOfWork;

        private const string _whSecret = "whsec_f64c6573c9581f7954bc29643804a4dc3d1ae2402219cd92450eb836437197e1";

        public PaymentsController(IPaymentService paymentService,
            ILogger<PaymentsController> logger,
            IConfiguration configuration,
            IBasketRepository basketRepository,
            UserManager<ApplicationUser> userManager,
            IMapper _mapper,
            IOrderService orderService,
            IUnitOfWork unitOfWork)
        {
            _paymentService = paymentService;
            _logger = logger;
            _configuration = configuration;
            _basketRepository = basketRepository;
            _userManager = userManager;
            this._mapper = _mapper;
            _orderService = orderService;
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        [ProducesResponseType(typeof(CustomerBasket), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost("{basketId}")] //{{BaseUrl}}/api/payments/123asd
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);

            if (basket is null)
            {
                return BadRequest(new ApiResponse(400, "An Error with your Basket"));
            }

            return Ok(basket);
        }


        [HttpPost("webhook")] // {{BaseUrl}}/api/payments/webhook
        public async Task<IActionResult> StripeWebhook() {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], _whSecret);

            var paymentIntent = (PaymentIntent)stripeEvent.Data.Object;

            Order order;

            switch (stripeEvent.Type)
            {
                case Events.PaymentIntentSucceeded:
                    order = await _paymentService.UpdatePaymentIntentToSucceededOrFailed(paymentIntent.Id, true);
                    _logger.LogInformation("Payment is succeeded ya Hamada :)", paymentIntent.Amount);
                    break;
                case Events.PaymentIntentPaymentFailed:
                    order = await _paymentService.UpdatePaymentIntentToSucceededOrFailed(paymentIntent.Id, false);
                    _logger.LogInformation("Payment is failed ya Hamada :(", paymentIntent.Amount);
                    break;
            }

            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> CreateCheckoutSession(string basketId, [FromQuery]string url, [FromBody] AddressDto shippingAddress)
        {
            var currency = "egp";
            var successUrl = $"{url}/successOrder/{basketId}";
            var failUrl = $"{url}/failOrder";
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];
            var basket = await _basketRepository.GetBasketAsync(basketId);
            decimal TotalAmount = 0;
            var currentUser = await _userManager.GetUserAsync(User);
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = currency,
                            UnitAmount =
                                (long)basket.BasketItems.Sum(item => item.ProductPrice * 100 * item.ProductQuantity * (item.ProductDiscount == 0 ? 1 : 1 - (item.ProductDiscount / 100))) +
                                (long)basket.ShippingPrice * 100,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = $"{currentUser.FirstName} {currentUser.LastName}",
                                Description = $"{currentUser.Email}"
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = failUrl
            };

            var service = new SessionService();

            var session = service.Create(options);

            var address = _mapper.Map<AddressDto, Core.Entities.Order_Aggregate.Address>(shippingAddress);
            var order = await _orderService.CreateOrderAsync(buyerEmail, basketId, (int)basket.DeliveryMethodId, address, session.Id);

            var newSuccessUrl = $"{successUrl}/{order.Id}";

            options.SuccessUrl = newSuccessUrl;

            session = service.Create(options);

            order.PaymentIntentId = session.Id;

            await _unitOfWork.CompleteAsync();

            return Ok(new
            {
                orderId = order.Id,
                sessionId = session.Id,
                stripeUrl = session.Url,
            });
        }



    }
}
