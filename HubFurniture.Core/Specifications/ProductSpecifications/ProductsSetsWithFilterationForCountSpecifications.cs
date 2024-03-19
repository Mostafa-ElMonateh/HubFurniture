using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HubFurniture.Core.Entities;

namespace HubFurniture.Core.Specifications.ProductSpecifications
{
    public class ProductsSetsWithFilterationForCountSpecifications : BaseSpecifications<CategorySet>
    {
        public ProductsSetsWithFilterationForCountSpecifications(ProductSpecParams specParams)
            :base(cs => 
                (!specParams.CategoryId.HasValue || cs.CategoryId == specParams.CategoryId) &&
                (string.IsNullOrEmpty(specParams.ProductColor) || cs.Color == specParams.ProductColor) &&
                (!specParams.SetTypeId.HasValue || cs.CategorySetTypeId == specParams.SetTypeId) &&
                (!specParams.MinimumPrice.HasValue || cs.Price >= specParams.MinimumPrice) &&
                (!specParams.MaximumPrice.HasValue || cs.Price <= specParams.MaximumPrice)
            )
        {
            
        }
    }
}
