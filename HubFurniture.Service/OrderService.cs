using HubFurniture.Core.Contracts;
using HubFurniture.Core.Contracts.Contracts.Repositories;
using HubFurniture.Core.Contracts.Contracts.Services;
using HubFurniture.Core.Entities;
using HubFurniture.Core.Entities.Order_Aggregate;
using HubFurniture.Core.Specifications.OrderSpecifications;
using HubFurniture.Core.Specifications.ProductPictureSpecifications;

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
                string setType = "set", itemType="item";
                CategorySet? set;
                CategoryItem? item;
                ProductItemOrdered productItemOrdered;
                OrderItem orderItem;
                IReadOnlyList<ProductPicture> pictures;
                var setsRepository = _unitOfWork.Repository<CategorySet>();
                var itemsRepository = _unitOfWork.Repository<CategoryItem>();
                var picturesRepository = _unitOfWork.Repository<ProductPicture>();
                SetPictureSpecifications setSpecifications;
                ItemPictureSpecifications itemSpecifications;
                foreach (var basketItem in basket.BasketItems)
                {

                    if (basketItem.Type == setType)
                    {
                        set = await setsRepository.GetByIdAsync(basketItem.ProductId);
                        setSpecifications = new SetPictureSpecifications(set.Id);
                        pictures = await picturesRepository.GetAllWithSpecAsync(setSpecifications);
                        productItemOrdered =
                            new ProductItemOrdered(basketItem.ProductId, set.NameArabic, set.NameEnglish, pictures[0].PictureUrl, setType);
                        orderItem = new OrderItem(productItemOrdered, set.Price, basketItem.ProductQuantity);
                    }
                    else
                    {
                        item = await itemsRepository.GetByIdAsync(basketItem.ProductId);
                        itemSpecifications = new ItemPictureSpecifications(item.Id);
                        pictures = await picturesRepository.GetAllWithSpecAsync(itemSpecifications);
                        productItemOrdered =
                            new ProductItemOrdered(basketItem.ProductId, item.NameArabic, item.NameEnglish, pictures[0].PictureUrl, itemType);
                        orderItem = new OrderItem(productItemOrdered, item.Price, basketItem.ProductQuantity);
                    }

                    orderItems.Add(orderItem);
                }
            }

            // 3. Calculate SubTotal.
            var subTotal = orderItems.Sum(orderItem => orderItem.Price * orderItem.Quantity);

            // 4. Get Delivery Method from DeliveryMethods Repo.
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

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

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var orderRepository = _unitOfWork.Repository<Order>();
            var specifications = new OrderSpecifications(buyerEmail);
            var orders = await orderRepository.GetAllWithSpecAsync(specifications);

            return orders;
        }

        public async Task<Order?> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            var orderRepository = _unitOfWork.Repository<Order>();
            var specifications = new OrderSpecifications(orderId, buyerEmail);
            var order = await orderRepository.GetWithSpecAsync(specifications);
            return order;
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            var deliveryMethodsRepository = _unitOfWork.Repository<DeliveryMethod>();
            var deliveryMethods = deliveryMethodsRepository.GetAllAsync();
            return deliveryMethods;
        }
    }
}
