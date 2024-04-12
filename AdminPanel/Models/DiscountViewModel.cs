using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models
{
    public class DiscountViewModel
    {
        [Required(ErrorMessage = "Discount is Required")]
        [Range(0, 100)]
        public decimal Discount { get; set; }

        [Required(ErrorMessage = "Type is Required")]
        public string Type { get; set; }
    }
}
