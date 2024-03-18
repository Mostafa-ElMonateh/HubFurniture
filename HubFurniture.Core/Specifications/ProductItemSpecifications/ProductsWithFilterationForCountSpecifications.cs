using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HubFurniture.Core.Entities;

namespace HubFurniture.Core.Specifications.ProductItemSpecifications
{
    public class ProductsWithFilterationForCountSpecifications : BaseSpecifications<ProductItem>
    {
        public ProductsWithFilterationForCountSpecifications(ProductSpecParams specParams)
            :base(pi => 
                (string.IsNullOrEmpty(specParams.Search) || pi.Name.ToLower().Contains(specParams.Search))&&
                (string.IsNullOrEmpty(specParams.CategoryName) || pi.Category.Name == specParams.CategoryName) &&
                (string.IsNullOrEmpty(specParams.ProductColor) || pi.Color == specParams.ProductColor) &&
                (!specParams.MinimumPrice.HasValue || pi.Price >= specParams.MinimumPrice) &&
                (!specParams.MaximumPrice.HasValue || pi.Price <= specParams.MaximumPrice)
            )
        {
            
        }
    }
}
