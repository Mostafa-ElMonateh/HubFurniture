using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Core.Entities.Order_Aggregate
{
    public class ProductItemOrdered
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }

        public ProductItemOrdered(int productId, string productName, string pictureUrl)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

        public ProductItemOrdered()
        {
            
        }
    }
}