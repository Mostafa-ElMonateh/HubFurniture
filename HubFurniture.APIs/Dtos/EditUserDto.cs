using System.ComponentModel.DataAnnotations;

namespace HubFurniture.APIs.Dtos
{
    public class EditUserDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
