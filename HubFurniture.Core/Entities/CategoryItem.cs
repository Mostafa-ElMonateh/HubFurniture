using HubFurniture.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HubFurniture.Core.Contracts.Contracts.Entities;
using System.Text.Json.Serialization;

namespace HubFurniture.Core.Entities
{
    public class CategoryItem : BaseEntity, IProduct
    {
        public string Name { get; set; }
        public Availability Availability { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string Style { get; set; }
        public Suitability Suitability { get; set; }
        public string Room { get; set; }
        public decimal? Height { get; set; }
        public decimal? Depth { get; set; }
        public decimal? Width { get; set; }
        public int CategoryItemTypeId { get; set; }

        // Navigational Property 1-M => [M]
        public List<ProductPicture> ProductPictures{ get; set; } = new List<ProductPicture>();
        
        // Navigational Property 1-M => [M]
        public ICollection<CustomerReview> CustomerReviews { get; set; } = new HashSet<CustomerReview>();
        
        
        public int CategoryId { get; set; }
        
        [JsonIgnore] // Avoid Circle Ref
        public Category Category { get; set; }
    }
}
