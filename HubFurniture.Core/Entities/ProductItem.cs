﻿using HubFurniture.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HubFurniture.Core.Contracts.Contracts.Entities;

namespace HubFurniture.Core.Entities
{
    public class ProductItem : BaseEntity, IProduct
    {
        public string Name { get; set; }
        public Availability Availability { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string Style { get; set; }
        public Suitability Suitability { get; set; }
        public string Room { get; set; }
        public decimal? Height { get; set; }
        public decimal? Depth { get; set; }
        public decimal? Width { get; set; }
        public ICollection<ProductPicture> ProductPictures{ get; set; } = new HashSet<ProductPicture>();
        public ICollection<CustomerReview> CustomerReviews { get; set; } = new HashSet<CustomerReview>();
        public ICollection<ProductCollection> ProductCollections { get; set; } = new HashSet<ProductCollection>();
    }
}