namespace HubFurniture.APIs.Dtos
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Availability { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string Style { get; set; }
        public string Suitability { get; set; }
        public string Room { get; set; }
        public IEnumerable<string> ProductPictures{ get; set; } = new HashSet<string>();
        public IEnumerable<CustomerReviewToReturnDto> CustomerReviews { get; set; } = new HashSet<CustomerReviewToReturnDto>();
    }
}
