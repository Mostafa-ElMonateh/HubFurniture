using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Core.Entities.Order_Aggregate
{
    public class ProductItemOrdered
    {
        private readonly int _setTypeId;
        public int ProductId { get; set; }
        public string ProductNameArabic { get; set; }
        public string ProductNameEnglish { get; set; }

        public string PictureUrl { get; set; }
        public string Type { get; set; }

        public ProductItemOrdered(int productId, string productNameArabic,  string productNameEnglish,  string pictureUrl, string type)
        {
            ProductId = productId;
            ProductNameArabic =   productNameArabic;
            ProductNameEnglish = productNameEnglish;
            PictureUrl = pictureUrl;
            Type = type;
        }

        public ProductItemOrdered()
        {
            
        }
    }
}