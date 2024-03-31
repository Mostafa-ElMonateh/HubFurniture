using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        // Navigational Property 1-M => [M]
        public ICollection<UserAddress> Addresses { get; set; } = new HashSet<UserAddress>();
        public  string? BasketId { get; set; }
    
        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        public ApplicationUser()
        {
            this.UserName = this.Id;
        }
    }
}
