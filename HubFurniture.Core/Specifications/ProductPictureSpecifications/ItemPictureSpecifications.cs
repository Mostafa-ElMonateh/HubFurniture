using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HubFurniture.Core.Entities;

namespace HubFurniture.Core.Specifications.ProductPictureSpecifications
{
    public class ItemPictureSpecifications : BaseSpecifications<ProductPicture>
    {
        public ItemPictureSpecifications(int itemId)
            :base(pp => pp.CategoryItemId == itemId)
        {

        }
    }
}
