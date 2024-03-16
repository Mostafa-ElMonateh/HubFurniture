using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Core.Entities
{
    public class ProductPicture : BaseEntity
    {
        public string PictureUrl { get; set; }
        public int? ProductCollectionId { get; set; }
        public int? ProductItemId { get; set; }
    }
}
