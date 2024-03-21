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
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public string Type { get; set; }

        public ProductItemOrdered(int productId, string productName, string pictureUrl, string type)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrl = pictureUrl;
            Type = type;
        }

        public ProductItemOrdered()
        {
            
        }
    }
}