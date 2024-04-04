using System.ComponentModel.DataAnnotations;
using AdminPanel.Validations;
using HubFurniture.Core.Entities;
using HubFurniture.Repository.Data;

namespace AdminPanel.Models
{
    public class ItemTypeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Arabic Name is Required")]
        [CheckExistingName<CategoryItemType>(typeof(StoreContext), "NameArabic")]
        public string NameArabic { get; set; }

        [Required(ErrorMessage = "English Name is Required")]
        [CheckExistingName<CategoryItemType>(typeof(StoreContext), "NameEnglish")]
        public string NameEnglish { get; set; }
        public int CategoryId { get; set; }
        public ItemCategoryViewModel? Category { get; set; }

    }
}
