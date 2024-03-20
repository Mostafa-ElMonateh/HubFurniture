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
        public ICollection<string> PictureUrls { get; set; }

        public ProductItemOrdered(int productId, string productName)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrls = new List<string>();
        }

        public ProductItemOrdered()
        {
            
        }
    }
}