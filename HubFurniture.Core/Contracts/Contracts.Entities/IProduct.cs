using HubFurniture.Core.Entities;
using HubFurniture.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Core.Contracts.Contracts.Entities
{
    public interface IProduct
    {
        public string Name { get; set; }
        public Availability Availability { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string Style { get; set; }
        public Suitability Suitability { get; set; }
        public string Room { get; set; }
        public int CategoryId { get; set; }
        public ICollection<ProductPicture> ProductPictures { get; set; }
        public ICollection<CustomerReview> CustomerReviews { get; set; }
    }
}
