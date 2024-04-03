using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models
{
    public class LoginViewModel
    {
        [Required]
        public required string email { get; set; }
        [Required]
        public required string password { get; set; }

    }
}
