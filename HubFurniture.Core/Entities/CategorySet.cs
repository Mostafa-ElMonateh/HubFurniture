using HubFurniture.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using HubFurniture.Core.Contracts.Contracts.Entities;

namespace HubFurniture.Core.Entities
{
    public class CategorySet: BaseEntity, IProduct
    {
        public string Name { get; set; }
        public Availability Availability { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string Style { get; set; }
        public Suitability Suitability { get; set; }
        public string Room { get; set; }
        public int CategorySetTypeId { get; set; }

        // Navigational Property 1-M => [M]
        public List<ProductPicture> ProductPictures{ get; set; } = new List<ProductPicture>();
        
        // Navigational Property 1-M => [M]
        public ICollection<CustomerReview> CustomerReviews { get; set; } = new HashSet<CustomerReview>();
        
        [JsonIgnore] // Avoid Circle Ref
        // Navigational Property M-M => [M]
        public ICollection<CategoryItem> CategoryItems { get; set; } = new HashSet<CategoryItem>();
        
        public int CategoryId { get; set; }

        // Navigational Property 1-M => [1]
        [JsonIgnore] // Avoid Circle Ref
        public Category Category { get; set; }


    }
}
