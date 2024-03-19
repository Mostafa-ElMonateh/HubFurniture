using AutoMapper;
using HubFurniture.APIs.Dtos;
using HubFurniture.Core.Entities;

namespace HubFurniture.APIs.Helpers
{
    public class ProductCategorySetsResolver : IValueResolver<Category, ProductCategoryToReturnDto, IEnumerable<CategoryTypesToReturnDto>>
    {
        public IEnumerable<CategoryTypesToReturnDto> Resolve(Category source, ProductCategoryToReturnDto destination, IEnumerable<CategoryTypesToReturnDto> destMember,
            ResolutionContext context)
        {
            if (source.CategorySetsTypes.Any())
            {
                return source.CategorySetsTypes.Select(cst => new CategoryTypesToReturnDto(){Id = cst.Id, Name = cst.Name});
            }

            return Enumerable.Empty<CategoryTypesToReturnDto>();
        }

    }
}
