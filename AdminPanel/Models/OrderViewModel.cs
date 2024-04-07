using HubFurniture.Core.Entities.Order_Aggregate;

namespace AdminPanel.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public Address? ShippingAddress { get; set; }
        public DeliveryMethod? DeliveryMethod { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public string PaymentIntentId { get; set; }
    }
}
