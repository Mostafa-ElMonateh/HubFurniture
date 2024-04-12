using HubFurniture.Core.Entities.Order_Aggregate;
using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models
{
    public class OrderToReturnViewModel
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public DeliveryMethod? DeliveryMethod { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public decimal SubTotal { get; set; }
    }
}
