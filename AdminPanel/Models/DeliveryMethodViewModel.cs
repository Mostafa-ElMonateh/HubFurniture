using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models
{
    public class DeliveryMethodViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description in Arabic is Required")]
        public string DescriptionArabic { get; set; }

        [Required(ErrorMessage = "Description in English is Required")]
        public string DescriptionEnglish { get; set; }

        [Required(ErrorMessage = "Cost is Required")]
        [Range(1, 1000)]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Delivery Time in Arabic is Required")]
        public string DeliveryTimeArabic { get; set; }

        [Required(ErrorMessage = "Delivery Time in English is Required")]
        public string DeliveryTimeEnglish{ get; set; }
    }
}
