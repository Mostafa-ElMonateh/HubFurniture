using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Core.Entities.Order_Aggregate
{
    public class Order : BaseEntity
    {
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Address ShippingAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; } // Navigational Property [One]
        
        // Navigational Property [M]
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        public decimal SubTotal { get; set; }

        public decimal GetTotal()
            => SubTotal + DeliveryMethod.Cost;

        public string PaymentIntentId { get; set; } = string.Empty;

        public Order(string buyerEmail,
            Address shippingAddress,
            DeliveryMethod deliveryMethod,
            ICollection<OrderItem> items,
            decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = items;
            SubTotal = subTotal;
        }

        public Order()
        {
            
        }
    }
}
