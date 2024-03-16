using HubFurniture.Core.Entities;
using HubFurniture.Core.Enums;

namespace HubFurniture.APIs.Dtos
{
    public class ProductItemToReturnDto
    {
        public int Id { get; set; }
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
        public IEnumerable<string> ProductPictures{ get; set; } = new HashSet<string>();
        public IEnumerable<CustomerReviewToReturnDto> CustomerReviews { get; set; } = new HashSet<CustomerReviewToReturnDto>();
    }
}
