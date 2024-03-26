using AutoMapper;
using HubFurniture.APIs.Dtos;
using HubFurniture.Core.Entities;

namespace HubFurniture.APIs.Helpers
{
    public class ProductCategoryItemsResolver : IValueResolver<Category, ProductCategoryToReturnDto, IEnumerable<CategoryTypesToReturnDto>>
    {
        public IEnumerable<CategoryTypesToReturnDto> Resolve(Category source, ProductCategoryToReturnDto destination, IEnumerable<CategoryTypesToReturnDto> destMember,
            ResolutionContext context)
        {
            if (source.CategoryItemsTypes.Any())
            {
                return source.CategoryItemsTypes.Select(cst => new CategoryTypesToReturnDto(){Id = cst.Id, NameArabic = cst.NameArabic, NameEnglish = cst.NameEnglish});
            }

            return Enumerable.Empty<CategoryTypesToReturnDto>();
        }
    }
}
