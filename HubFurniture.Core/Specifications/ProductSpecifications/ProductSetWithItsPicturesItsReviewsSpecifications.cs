using HubFurniture.Core.Entities;

namespace HubFurniture.Core.Specifications.ProductSpecifications
{
    public class ProductSetWithItsPicturesItsReviewsSpecifications : BaseSpecifications<CategorySet>
    {

        // This Constructor will be used for creating an Object, that will be used to get all productSets
        public ProductSetWithItsPicturesItsReviewsSpecifications(ProductSpecParams specParams)
            :base(cs => 
                    (string.IsNullOrEmpty(specParams.Search) || cs.Name.ToLower().Contains(specParams.Search))&&
                    (!specParams.SetTypeId.HasValue || cs.CategorySetTypeId == specParams.SetTypeId) &&
                    (!specParams.CategoryId.HasValue || cs.CategoryId == specParams.CategoryId) &&
                    (string.IsNullOrEmpty(specParams.ProductColor) || cs.Color == specParams.ProductColor) &&
                    (!specParams.MinimumPrice.HasValue || cs.Price >= specParams.MinimumPrice) &&
                    (!specParams.MaximumPrice.HasValue || cs.Price <= specParams.MaximumPrice)
                )
        {
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
                        AddOrderBy(cs => cs.Name);
                        break;
                    case "nameDesc":
                        AddOrderByDesc(cs => cs.Name);
                        break;
                    default:
                        AddOrderBy(cs => cs.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(cs => cs.Name);
            }

            ApplyPagination((specParams.PageIndex - 1) * specParams.PageSize,specParams.PageSize);
            
        }

        // This Constructor will be used for creating an Object, that will be used to get a specific productItem
        public ProductSetWithItsPicturesItsReviewsSpecifications(int id)
            :base(p => p.Id == id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            Includes.Add(cs => cs.ProductPictures);
            Includes.Add(cs => cs.CustomerReviews);
            Includes.Add(cs => cs.CategoryItems);
            
        }
    }
}
