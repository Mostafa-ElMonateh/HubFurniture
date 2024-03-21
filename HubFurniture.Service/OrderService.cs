﻿using HubFurniture.Core.Contracts;
using HubFurniture.Core.Contracts.Contracts.Repositories;
using HubFurniture.Core.Contracts.Contracts.Services;
using HubFurniture.Core.Entities;
using HubFurniture.Core.Entities.Order_Aggregate;

namespace HubFurniture.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IBasketRepository basketRepository,
            IUnitOfWork unitOfWork)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress)
        {
            // 1. Get Basket from Baskets Repo
            var basket = await _basketRepository.GetBasketAsync(basketId);

            // 2. Get Selected Items at Basket from Products Repo
            var orderItems = new List<OrderItem>();

            if (basket?.BasketItems?.Count > 0)
            {
                CategorySet? set;
                CategoryItem? item;
                ProductItemOrdered productItemOrdered;
                OrderItem orderItem;
                List<ProductPicture> pictures;
                var setsRepository = _unitOfWork.Repository<CategorySet>();
                var itemsRepository = _unitOfWork.Repository<CategoryItem>();
                var picturesRepository = _unitOfWork.Repository<ProductPicture>();
                foreach (var basketItem in basket.BasketItems)
                {

                    if (basketItem.Type == "set")
                    {
                        set = await setsRepository.GetAsync(basketItem.ProductId);
                        pictures = await picturesRepository.GetAllWithCredentialAsync(pp => pp.CategorySetId == set.Id);
                        productItemOrdered =
                            new ProductItemOrdered(basketItem.ProductId, set.Name, pictures[0].PictureUrl);
                        orderItem = new OrderItem(productItemOrdered, set.Price, basketItem.ProductQuantity);
                    }
                    else
                    {
                        item = await itemsRepository.GetAsync(basketItem.ProductId);
                        pictures = await picturesRepository.GetAllWithCredentialAsync(pp => pp.CategoryItemId == item.Id);
                        productItemOrdered =
                            new ProductItemOrdered(basketItem.ProductId, item.Name, pictures[0].PictureUrl);
                        orderItem = new OrderItem(productItemOrdered, item.Price, basketItem.ProductQuantity);
                    }

                    orderItems.Add(orderItem);
                }
            }

            // 3. Calculate SubTotal.
            var subTotal = orderItems.Sum(orderItem => orderItem.Price * orderItem.Quantity);

            // 4. Get Delivery Method from DeliveryMethods Repo.
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(deliveryMethodId);

            // 5. Create Order.
            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, subTotal);

            await _unitOfWork.Repository<Order>().AddAsync(order);

            // 6. Save to Database
            var result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
            {
                return null;
            }

            return order;

        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
