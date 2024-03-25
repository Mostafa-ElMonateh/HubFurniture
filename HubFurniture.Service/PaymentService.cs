using HubFurniture.Core.Contracts;
using HubFurniture.Core.Contracts.Contracts.Repositories;
using HubFurniture.Core.Contracts.Contracts.Services;
using HubFurniture.Core.Entities;
using HubFurniture.Core.Entities.Order_Aggregate;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace HubFurniture.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;

        public PaymentService(IConfiguration configuration,
            IUnitOfWork unitOfWork,
            IBasketRepository basketRepository)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
        }
        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];

            var basket = await _basketRepository.GetBasketAsync(basketId);

            if (basket is null)
            {
                return null;
            }

            var shippingPrice = 0m;

            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(basket.DeliveryMethodId.Value);
                basket.ShippingPrice = deliveryMethod.Cost;
                shippingPrice = deliveryMethod.Cost;
            }

            if (basket?.BasketItems.Count > 0)
            {
                foreach (var item in basket.BasketItems)
                {
                    if (item.Type == "set")
                    {
                        var product = await _unitOfWork.Repository<CategorySet>().GetByIdAsync(item.ProductId);
                        if (item.ProductPrice != product.Price)
                        {
                            item.ProductPrice = product.Price;
                        }
                    }
                    else if (item.Type == "item")
                    {
                        var product = await _unitOfWork.Repository<CategoryItem>().GetByIdAsync(item.ProductId);
                        if (item.ProductPrice != product.Price)
                        {
                            item.ProductPrice = product.Price;
                        }
                    }
                }
            }

            PaymentIntentService paymentIntentService = new PaymentIntentService();

            PaymentIntent paymentIntent;

            if (string.IsNullOrEmpty(basket.PaymentIntentId)) // Create New Payment Intent
            {
                var createOptions = new PaymentIntentCreateOptions()
                {
                    Amount = (long) basket.BasketItems.Sum(item => item.ProductPrice * 100 * item.ProductQuantity) + (long) shippingPrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new(){"card"}
                };

                paymentIntent = await paymentIntentService.CreateAsync(createOptions);

                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else // Update New Payment Intent
            {
                var updateOptions = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.BasketItems.Sum(item => item.ProductPrice * 100 * item.ProductQuantity) +
                             (long)shippingPrice * 100,
                };
                await paymentIntentService.UpdateAsync(basket.PaymentIntentId, updateOptions);
            }

            await _basketRepository.UpdateBasketAsync(basket);

            return basket;

        }
    }
}
