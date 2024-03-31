using HubFurniture.Core.Entities;
using System.Globalization;

namespace HubFurniture.Core.Specifications.ProductSpecifications
{
    public class SetWithItsPicturesItsReviewsSpecifications : BaseSpecifications<CategorySet>
    {
         string currentCulture;

        // This Constructor will be used for creating an Object, that will be used to get all productSets
        public SetWithItsPicturesItsReviewsSpecifications(ProductSpecParams specParams)
            :base(cs => 
                    (string.IsNullOrEmpty(specParams.Search) ||
             (CultureInfo.CurrentCulture.Name == "ar" ? cs.NameArabic.ToLower().Contains(specParams.Search) : cs.NameEnglish.ToLower().Contains(specParams.Search))) &&
                    (!specParams.SetTypeId.HasValue || cs.CategorySetTypeId == specParams.SetTypeId) &&
                    (!specParams.CategoryId.HasValue || cs.CategoryId == specParams.CategoryId) &&
                    (string.IsNullOrEmpty(specParams.ProductColor) || cs.Color == specParams.ProductColor) &&
                    (!specParams.MinimumPrice.HasValue || cs.Price >= specParams.MinimumPrice) &&
                    (!specParams.MaximumPrice.HasValue || cs.Price <= specParams.MaximumPrice)
                )
        {
            currentCulture = CultureInfo.CurrentCulture.Name;
            AddIncludes();

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                { 
                    case "priceAsc":
                        AddOrderBy(cs => cs.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(cs => cs.Price);
                        break;
                    case "nameAsc":
                        AddOrderBy(cs => currentCulture == "ar" ? cs.NameArabic : cs.NameEnglish);
                        break;
                    case "nameDesc":
                        AddOrderByDesc(cs => currentCulture == "ar" ? cs.NameArabic : cs.NameEnglish);
                        break;
                    default:
                        AddOrderBy(cs => currentCulture == "ar" ? cs.NameArabic : cs.NameEnglish);
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
        public SetWithItsPicturesItsReviewsSpecifications(int id)
            :base(p => p.Id == id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            Includes.Add(cs => cs.ProductPictures);
            Includes.Add(cs => cs.CustomerReviews);
            Includes.Add(cs => cs.Items);
            
        }
    }
}
