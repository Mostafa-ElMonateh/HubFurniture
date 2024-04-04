using AdminPanel.Validations;
using HubFurniture.Core.Entities;
using System.ComponentModel.DataAnnotations;
using HubFurniture.Core.Contracts;
using HubFurniture.Repository.Data;

namespace AdminPanel.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "English Name is Required")]
        [CheckExistingName<Category>(typeof(StoreContext), "NameEnglish")]
        public string NameEnglish { get; set; }

        [Required(ErrorMessage = "Arabic Name is Required")]
        [CheckExistingName<Category>(typeof(StoreContext), "NameArabic")]
        public string NameArabic { get; set; }

    }
}
