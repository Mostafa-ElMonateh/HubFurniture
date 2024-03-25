﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Core.Entities
{
    public class Address
    {
        public required string StreetAdress { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
        public string? PostalCode { get; set; }
        public int UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
