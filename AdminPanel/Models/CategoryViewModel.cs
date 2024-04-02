using System.ComponentModel.DataAnnotations;
using HubFurniture.Core.Entities;

namespace AdminPanel.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Arabic Name is Required")]
        public string NameArabic { get; set; }

        [Required(ErrorMessage = "English Name is Required")]
        public string NameEnglish { get; set; }

    }
}
