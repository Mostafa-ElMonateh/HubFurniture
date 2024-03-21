using System.ComponentModel.DataAnnotations;

namespace HubFurniture.APIs.Dtos
{
    public class BasketItemDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal ProductPrice { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1 item")]
        public int ProductQuantity { get; set; }
        [Required]
        public string ProductPictureUrl { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Type { get; set; }
    }
}