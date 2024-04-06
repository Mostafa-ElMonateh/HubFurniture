using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Core.Entities.Order_Aggregate
{
    public class OrderItem : BaseEntity
    {
        public ProductItemOrdered ProductOrdered { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public OrderItem(ProductItemOrdered productOrdered, decimal price, decimal discount, int quantity)
        {
            ProductOrdered = productOrdered;
            Price = price;
            Discount = discount;
            Quantity = quantity;
        }

        public OrderItem()
        {
            
        }

    }
}
