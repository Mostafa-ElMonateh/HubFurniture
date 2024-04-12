using AdminPanel.Validations;
using HubFurniture.Core.Entities;
using HubFurniture.Core.Enums;
using HubFurniture.Repository.Data;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AdminPanel.Models
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public IFormFile? Image { get; set; }

        [CheckExistingName<CategoryItem>(typeof(StoreContext), "NameArabic")]
        [Required(ErrorMessage = "Arabic Name is Required")]
        public string NameArabic { get; set; }

        [CheckExistingName<CategoryItem>(typeof(StoreContext), "NameEnglish")]
        [Required(ErrorMessage = "English Name is Required")]
        public string NameEnglish { get; set; }

        [Required(ErrorMessage = "Availability is Required")]
        public Availability Availability { get; set; }

        [Required(ErrorMessage = "Price is Required")]
        [Range(1, 500000)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Discount is Required")]
        [Range(0, 100)]
        public decimal Discount { get; set; }

        [Required(ErrorMessage = "Color is Required")]
        public string Color { get; set; }
        [Required(ErrorMessage = "Style in Arabic is Required")]
        public string StyleArabic { get; set; }
        [Required(ErrorMessage = "Style in English is Required")]
        public string StyleEnglish { get; set; }
        [Required(ErrorMessage = "Suitability is Required")]
        public Suitability Suitability { get; set; }
        [Required(ErrorMessage = "Room in Arabic is Required")]
        public string RoomArabic { get; set; }
        [Required(ErrorMessage = "Room in English is Required")]
        public string RoomEnglish { get; set; }
        [Required(ErrorMessage = "Height is Required")]
        [Range(50, 400)]
        public decimal? Height { get; set; }
        [Required(ErrorMessage = "Depth is Required")]
        [Range(50, 400)]
        public decimal? Depth { get; set; }
        [Required(ErrorMessage = "Width is Required")]
        [Range(50, 400)]
        public decimal? Width { get; set; }

        [Required(ErrorMessage = "Type is Required")]
        public int CategoryItemTypeId { get; set; }

        public ItemTypeToReturnViewModel? CategoryItemType { get; set; }
        // Navigational Property 1-M => [M]
        public List<ProductPicture> ProductPictures{ get; set; } = new List<ProductPicture>();
        // Navigational Property 1-M => [M]
        
        public ICollection<CustomerReview>? CustomerReviews { get; set; }
        
        [Required(ErrorMessage = "Category is Required")]
        public int CategoryId { get; set; }


        [JsonIgnore] // Avoid Circle Ref
        public ItemCategoryViewModel? Category { get; set; }
    }
}
