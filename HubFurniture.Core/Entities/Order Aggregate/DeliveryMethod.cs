using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Core.Entities.Order_Aggregate
{
    public class DeliveryMethod : BaseEntity
    {
        public string Name { get; set; }
        public string DescriptionArabic { get; set; }
        public string DescriptionEnglish { get; set; }
        public decimal Cost { get; set; }
        public string DeliveryTimeArabic { get; set; }
        public string DeliveryTimeEnglish{ get; set; }
        public DeliveryMethod(string name, string descriptionArabic, string descriptionEnglish, decimal cost, string deliveryTimeArabic, string deliveryTimeEnglish)
        {
            Name = name;
            DescriptionArabic = descriptionArabic;
            DescriptionEnglish = descriptionEnglish;
            Cost = cost;
            DeliveryTimeArabic = deliveryTimeArabic;
            DeliveryTimeEnglish = deliveryTimeEnglish;
        }

        public DeliveryMethod()
        {
            
        }
    }
}
