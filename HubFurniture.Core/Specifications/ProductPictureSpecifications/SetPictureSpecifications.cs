using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HubFurniture.Core.Entities;

namespace HubFurniture.Core.Specifications.ProductPictureSpecifications
{
    public class SetPictureSpecifications : BaseSpecifications<ProductPicture>
    {
        public SetPictureSpecifications(int setId)
            :base(pp => pp.CategoryItemId == setId)
        {

        }
    }
}
