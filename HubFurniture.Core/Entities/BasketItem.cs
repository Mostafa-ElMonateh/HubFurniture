﻿namespace HubFurniture.Core.Entities
{
    public class BasketItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductPictureUrl { get; set; }
        public string Category { get; set; }
    }
}