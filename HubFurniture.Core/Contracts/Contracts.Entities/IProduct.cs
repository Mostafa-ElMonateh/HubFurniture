using HubFurniture.Core.Entities;
using HubFurniture.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Core.Contracts.Contracts.Entities
{
    public interface IProduct
    {
        public string NameArabic { get; set; }
        public string NameEnglish { get; set; }
        public Availability Availability { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string StyleArabic { get; set; }
        public string StyleEnglish { get; set; }
        public Suitability Suitability { get; set; }
        public string RoomArabic { get; set; }
        public string RoomEnglish { get; set; }

        public int? CategoryId { get; set; }
        public List<ProductPicture> ProductPictures { get; set; }
        public ICollection<CustomerReview> CustomerReviews { get; set; }
    }
}
