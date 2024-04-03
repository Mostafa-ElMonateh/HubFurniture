using System.Globalization;
using System.Security.Claims;
using AutoMapper;
using HubFurniture.APIs.Dtos;
using HubFurniture.APIs.Errors;
using HubFurniture.Core.Contracts;
using HubFurniture.Core.Contracts.Contracts.Services;
using HubFurniture.Core.Entities.Order_Aggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace HubFurniture.APIs.Controllers
{
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        string currentCulture;


        public OrdersController(IOrderService orderService,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _orderService = orderService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            currentCulture = CultureInfo.CurrentCulture.Name;
        }


        [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost("{orderId}")] // {{BaseUrl}}/api/orders/123
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder([FromRoute]int orderId)
        {

            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var order = await _orderService.GetOrderByIdForUserAsync(orderId, buyerEmail);

            var service = new SessionService();

            var session = service.Get(order.PaymentIntentId);

            if (session.PaymentStatus == "paid")
            {
                if (order is null)
                {
                    return BadRequest(new ApiResponse(400));
                }

                order.Status = OrderStatus.PaymentReceived;

                await _unitOfWork.CompleteAsync();

                return Ok(_mapper.Map<Order, OrderToReturnDto>(order));
            }
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet] // {{BaseUrl}}/api/orders
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
        {
            bool culture = currentCulture.StartsWith("ar");
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.GetOrdersForUserAsync(buyerEmail);
            dynamic mappedOrders;
            if (!culture)
            {
                mappedOrders = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderEnToReturnDto>>(orders);
            }
            else
            {
                mappedOrders = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderArToReturnDto>>(orders);
            }
            
            return Ok(mappedOrders);
        }

        [ProducesResponseType(typeof(OrderToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")] // {{BaseUrl}}/api/orders/1
        public async Task<ActionResult<OrderToReturnDto>> GetOrderForUser(int id)
        {
            bool culture = currentCulture.StartsWith("ar");
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderService.GetOrderByIdForUserAsync(id, buyerEmail);
            if (order is null)
            {
                return NotFound(new ApiResponse(404));
            }

            if (!culture)
            {
                return Ok(_mapper.Map<Order, OrderEnToReturnDto>(order));
            }
            return Ok(_mapper.Map<Order, OrderArToReturnDto>(order));
            
        }

        [HttpGet("deliveryMethods")] // {{BaseUrl}}/api/deliveryMethod
        public async Task<ActionResult<IReadOnlyList<DeliveryMethodDto>>> GetDeliveryMethods()
        {
            bool checkCulture = currentCulture.StartsWith("ar");
            var deliveryMethods = await _orderService.GetDeliveryMethodsAsync();
            var deliveryMethodDtos = deliveryMethods.Select(method => new DeliveryMethodDto
            {
                Id = method.Id,
                Name = method.Name,
                Description = checkCulture ? method.DescriptionArabic : method.DescriptionEnglish,
                Cost = method.Cost,
                DeliveryTime = checkCulture ? method.DeliveryTimeArabic : method.DeliveryTimeEnglish
            }).ToList();

            return Ok(deliveryMethodDtos);
        }


        [HttpPost("cancel/{orderId}")] // {{BaseUrl}}/api/orders/123
        public async Task<ActionResult> CancelOrder([FromRoute]int orderId)
        {
            bool culture = currentCulture.StartsWith("ar");
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderService.GetOrderByIdForUserAsync(orderId, buyerEmail);
            if (order is null)
            {
                return NotFound(new ApiResponse(404));
            }

            order.Status = OrderStatus.Cancelled;

            await _unitOfWork.CompleteAsync();

            return Ok();

        }

    }
}
