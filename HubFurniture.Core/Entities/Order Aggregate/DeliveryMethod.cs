using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Core.Entities.Order_Aggregate
{
    public class DeliveryMethod : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public string DeliveryTime { get; set; }

        public DeliveryMethod(string name, string description, string deliveryTime)
        {
            Name = name;
            Description = description;
            DeliveryTime = deliveryTime;
        }

        public DeliveryMethod()
        {
            
        }
    }
}
