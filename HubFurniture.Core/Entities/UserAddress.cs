using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Core.Entities
{
    public class UserAddress : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string PhoneNumber { get; set; }
        public string Fax { get; set; }
        public required string StreetAdress { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
        public string? PostalCode { get; set; }
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
