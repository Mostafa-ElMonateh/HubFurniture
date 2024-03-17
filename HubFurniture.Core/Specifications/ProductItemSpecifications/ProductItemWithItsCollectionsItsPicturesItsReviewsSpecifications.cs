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
        public ProductItemWithItsCollectionsItsPicturesItsReviewsSpecifications(string? sort, string? categoryName, string? productColor, int? minimumPrice, int? maximumPrice)
            :base(pi => 
                
                    (string.IsNullOrEmpty(categoryName) || pi.Category.Name == categoryName) &&
                    (string.IsNullOrEmpty(productColor) || pi.Color == productColor) &&
                    (!minimumPrice.HasValue || pi.Price >= minimumPrice) &&
                    (!maximumPrice.HasValue || pi.Price <= maximumPrice)
                
                )
        {
            AddIncludes();

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
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
