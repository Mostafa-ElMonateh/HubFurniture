using HubFurniture.Core.Entities.Order_Aggregate;


namespace HubFurniture.Core.Specifications.OrderSpecifications
{
    public class OrderSpecifications : BaseSpecifications<Order>
    {
        public OrderSpecifications(string buyerEmail)
        : base(o => o.BuyerEmail == buyerEmail)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.OrderItems);
            AddOrderByDesc(o => o.OrderDate);
        }

        public OrderSpecifications(int orderId, string buyerEmail)
            : base(o => o.BuyerEmail == buyerEmail && o.Id == orderId)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.OrderItems);
        }
        public OrderSpecifications()
        {
            Includes.Add(o => o.DeliveryMethod);
        }

        public OrderSpecifications(int id)
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.OrderItems);
            Includes.Add(o => o.ShippingAddress);
            AddOrderByDesc(o => o.OrderDate);
        }
    }
}
