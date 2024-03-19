using HubFurniture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Core.Specifications.ProductSpecifications
{
    public class ProductItemWithItsPicturesItsReviewsSpecifications : BaseSpecifications<CategoryItem>
    {
        // This Constructor will be used for creating an Object, that will be used to get all productItems
        public ProductItemWithItsPicturesItsReviewsSpecifications(ProductSpecParams specParams)
            :base(ci => 
                (string.IsNullOrEmpty(specParams.Search) || ci.Name.ToLower().Contains(specParams.Search))&&
                (!specParams.ItemTypeId.HasValue || ci.CategoryItemTypeId == specParams.ItemTypeId) &&
                (!specParams.CategoryId.HasValue || ci.CategoryId == specParams.CategoryId) &&
                (string.IsNullOrEmpty(specParams.ProductColor) || ci.Color == specParams.ProductColor) &&
                (!specParams.MinimumPrice.HasValue || ci.Price >= specParams.MinimumPrice) &&
                (!specParams.MaximumPrice.HasValue || ci.Price <= specParams.MaximumPrice)
            )
        {
            AddIncludes();

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                { 
                    case "priceAsc":
                        AddOrderBy(ci => ci.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(ci => ci.Price);
                        break;
                    case "nameAsc":
                        AddOrderBy(ci => ci.Name);
                        break;
                    case "nameDesc":
                        AddOrderByDesc(ci => ci.Name);
                        break;
                    default:
                        AddOrderBy(ci => ci.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(ci => ci.Name);
            }

            ApplyPagination((specParams.PageIndex - 1) * specParams.PageSize,specParams.PageSize);
            
        }

        // This Constructor will be used for creating an Object, that will be used to get a specific productItem
        public ProductItemWithItsPicturesItsReviewsSpecifications(int id)
            :base(p => p.Id == id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            Includes.Add(ci => ci.ProductPictures);
            Includes.Add(ci => ci.CustomerReviews);
            
        }
    }
}
