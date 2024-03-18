using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HubFurniture.Core.Entities;

namespace HubFurniture.Core.Specifications.ProductItemSpecifications
{
    public class ProductItemWithItsCollectionsItsPicturesItsReviewsSpecifications : BaseSpecifications<ProductItem>
    {

        // This Constructor will be used for creating an Object, that will be used to get all productItems
        public ProductItemWithItsCollectionsItsPicturesItsReviewsSpecifications(ProductSpecParams specParams)
            :base(pi => 
                    (string.IsNullOrEmpty(specParams.Search) || pi.Name.ToLower().Contains(specParams.Search))&&
                    (string.IsNullOrEmpty(specParams.CategoryName) || pi.Category.Name == specParams.CategoryName) &&
                    (string.IsNullOrEmpty(specParams.ProductColor) || pi.Color == specParams.ProductColor) &&
                    (!specParams.MinimumPrice.HasValue || pi.Price >= specParams.MinimumPrice) &&
                    (!specParams.MaximumPrice.HasValue || pi.Price <= specParams.MaximumPrice)
                )
        {
            AddIncludes();

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                { 
                    case "priceAsc":
                        AddOrderBy(pi => pi.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(pi => pi.Price);
                        break;
                    case "nameAsc":
                        AddOrderBy(pi => pi.Name);
                        break;
                    case "nameDesc":
                        AddOrderByDesc(pi => pi.Name);
                        break;
                    default:
                        AddOrderBy(pi => pi.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(pi => pi.Name);
            }

            ApplyPagination((specParams.PageIndex - 1) * specParams.PageSize,specParams.PageSize);
            
        }

        // This Constructor will be used for creating an Object, that will be used to get a specific productItem
        public ProductItemWithItsCollectionsItsPicturesItsReviewsSpecifications(int id)
            :base(p => p.Id == id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            Includes.Add(pi => pi.ProductCollections);
            Includes.Add(pi => pi.ProductPictures);
            Includes.Add(pi => pi.CustomerReviews);
        }
    }
}
