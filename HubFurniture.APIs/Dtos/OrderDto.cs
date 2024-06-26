﻿using System.ComponentModel.DataAnnotations;
using HubFurniture.Core.Entities.Order_Aggregate;

namespace HubFurniture.APIs.Dtos
{
    public class OrderDto
    {
        [Required]
        public string BasketId { get; set; }
        [Required]
        public int DeliveryMethodId { get; set; }
        [Required]
        public AddressDto ShippingAddress { get; set; }

    }
}
