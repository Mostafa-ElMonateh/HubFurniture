using System.ComponentModel.DataAnnotations;

namespace HubFurniture.APIs.Dtos
{
    public class ChangePasswordDto
    {
        [Required]
        public string currentPassword { get; set; }
        [Required]
        public string newPassword { get; set; }
        [Required]
        public string confirmPassword { get; set; }

    }
}
