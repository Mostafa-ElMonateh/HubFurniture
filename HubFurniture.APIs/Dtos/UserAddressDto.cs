using System.ComponentModel.DataAnnotations;

namespace HubFurniture.APIs.Dtos
{
    public class UserAddressDto
    {
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string Fax { get; set; }
        [Required]
        public string StreetAdress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }

        public string PostalCode { get; set; }
    }
}
