﻿using HubFurniture.Core.Enums;

namespace HubFurniture.APIs.Dtos
{
    public class ProductFlashCardToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Availability { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public IEnumerable<string> ProductPictures{ get; set; } = new HashSet<string>();
    }
}
