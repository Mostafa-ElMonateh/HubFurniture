using System.ComponentModel.DataAnnotations;

namespace HubFurniture.APIs.Dtos
{
    public class ChangeEmailDto
    { 
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
