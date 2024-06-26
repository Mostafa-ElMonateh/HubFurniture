﻿using HubFurniture.Core.Entities;
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
                    (!specParams.MinimumPrice.HasValue || cs.Price * (cs.Discount == 0 ? 1 : (1 - (cs.Discount / 100))) >= specParams.MinimumPrice) &&
                    (!specParams.MaximumPrice.HasValue || cs.Price * (cs.Discount == 0 ? 1 : (1 - (cs.Discount / 100))) <= specParams.MaximumPrice)
                )
        {
            currentCulture = CultureInfo.CurrentCulture.Name;
            AddIncludes();

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                { 
                    case "priceAsc":
                        AddOrderBy(cs => cs.Price * (cs.Discount == 0 ? 1 : (1 - (cs.Discount / 100))));
                        break;
                    case "priceDesc":
                        AddOrderByDesc(cs => cs.Price * (cs.Discount == 0 ? 1 : (1 - (cs.Discount / 100))));
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

        public SetWithItsPicturesItsReviewsSpecifications() : base()
        {
            AddOrderByDesc(i => i.Id);
            Includes.Add(ci => ci.ProductPictures);
            Includes.Add(ci => ci.Category);
            Includes.Add(ci => ci.CategorySetType);
            Includes.Add(ci => ci.Items);
        }

        private void AddIncludes()
        {
            Includes.Add(cs => cs.ProductPictures);
            Includes.Add(cs => cs.CustomerReviews);
            Includes.Add(cs => cs.Items);
            Includes.Add(cs => cs.Category);
            Includes.Add(cs => cs.CategorySetType);


        }
    }
}
