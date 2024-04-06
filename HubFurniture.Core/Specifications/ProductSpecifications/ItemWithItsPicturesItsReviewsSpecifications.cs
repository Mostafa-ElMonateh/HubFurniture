using HubFurniture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubFurniture.Core.Specifications.ProductSpecifications
{
    public class ItemWithItsPicturesItsReviewsSpecifications : BaseSpecifications<CategoryItem>
    {
        // localization
        string currentCulture;

        // This Constructor will be used for creating an Object, that will be used to get all productItems
        public ItemWithItsPicturesItsReviewsSpecifications(ProductSpecParams specParams)
            :base(ci => 
                (string.IsNullOrEmpty(specParams.Search) ||
             (CultureInfo.CurrentCulture.Name == "ar" ? ci.NameArabic.ToLower().Contains(specParams.Search) : ci.NameEnglish.ToLower().Contains(specParams.Search))) &&
                (!specParams.ItemTypeId.HasValue || ci.CategoryItemTypeId == specParams.ItemTypeId) &&
                (!specParams.CategoryId.HasValue || ci.CategoryId == specParams.CategoryId) &&
                (string.IsNullOrEmpty(specParams.ProductColor) || ci.Color == specParams.ProductColor) &&
                (!specParams.MinimumPrice.HasValue || (ci.Price * (ci.Discount == 0 ? 1 : (1 - (ci.Discount / 100)))) >= specParams.MinimumPrice) &&
                (!specParams.MaximumPrice.HasValue || (ci.Price * (ci.Discount == 0 ? 1 : (1 - (ci.Discount / 100)))) <= specParams.MaximumPrice)
            )
        {
            currentCulture = CultureInfo.CurrentCulture.Name;
            AddIncludes();

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                { 
                    case "priceAsc":
                        AddOrderBy(ci => ci.Price * (ci.Discount == 0 ? 1 : (1 - (ci.Discount / 100))));
                        break;
                    case "priceDesc":
                        AddOrderByDesc(ci => ci.Price * (ci.Discount == 0 ? 1 : (1 - (ci.Discount / 100))));
                        break;
                    case "nameAsc":
                        AddOrderBy(ci => currentCulture == "ar" ? ci.NameArabic : ci.NameEnglish);
                        break;
                    case "nameDesc":
                        AddOrderByDesc(ci => currentCulture == "ar" ? ci.NameArabic : ci.NameEnglish);
                        break;
                    default:
                        AddOrderBy(ci => currentCulture == "ar" ? ci.NameArabic : ci.NameEnglish);
                        break;
                }
            }
            else
            {
                AddOrderBy(ci => currentCulture == "ar" ? ci.NameArabic : ci.NameEnglish);
            }

            ApplyPagination((specParams.PageIndex - 1) * specParams.PageSize,specParams.PageSize);
            
        }

        // This Constructor will be used for creating an Object, that will be used to get a specific productItem
        public ItemWithItsPicturesItsReviewsSpecifications(int id)
            :base(p => p.Id == id)
        {
            AddIncludes();
            Includes.Add(ci => ci.Category);
            Includes.Add(ci => ci.CategoryItemType);
        }

        public ItemWithItsPicturesItsReviewsSpecifications():base()
        {
            AddOrderByDesc(i => i.Id);
            Includes.Add(ci => ci.ProductPictures);
            Includes.Add(ci => ci.Category);
            Includes.Add(ci => ci.CategoryItemType);
        }

        private void AddIncludes()
        {
            Includes.Add(ci => ci.ProductPictures);
            Includes.Add(ci => ci.CustomerReviews);
        }
    }
}
