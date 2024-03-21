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
        public int Quantity { get; set; }
        public OrderItem(ProductItemOrdered productOrdered, decimal price, int quantity)
        {
            ProductOrdered = productOrdered;
            Price = price;
            Quantity = quantity;
        }

        public OrderItem()
        {
            
        }

    }
}
