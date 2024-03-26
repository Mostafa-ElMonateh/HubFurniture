namespace HubFurniture.APIs.Dtos
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductNameArabic { get; set; }
        public string ProductNameEnglish { get; set; }

        public string PictureUrl { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        
    }
}