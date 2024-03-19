using AutoMapper;
using HubFurniture.APIs.Dtos;
using HubFurniture.Core.Entities;

namespace HubFurniture.APIs.Helpers
{
    public class CategorySetsResolver : IValueResolver<Category, CategorySetsToReturnDto, IEnumerable<CategoryTypesToReturnDto>>
    {
        public IEnumerable<CategoryTypesToReturnDto> Resolve(Category source, CategorySetsToReturnDto destination, IEnumerable<CategoryTypesToReturnDto> destMember,
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
