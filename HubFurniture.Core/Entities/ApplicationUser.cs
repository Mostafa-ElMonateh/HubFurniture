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
        public ICollection<Address> Addresses { get; set; } = new HashSet<Address>();
    }
}
