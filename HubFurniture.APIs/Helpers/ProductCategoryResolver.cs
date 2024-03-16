using AutoMapper;
using HubFurniture.APIs.Dtos;
using HubFurniture.Core.Entities;

namespace HubFurniture.APIs.Helpers
{
    public class ProductCategoryResolver : IValueResolver<Category, ProductCategoryToReturnDto, IEnumerable<string>>
    {
        public IEnumerable<string> Resolve(Category source, ProductCategoryToReturnDto destination, IEnumerable<string> destMember,
            ResolutionContext context)
        {
            if (source.CategorySets.Any())
            {
                return source.CategorySets.Select(cs => cs.Name);
            }

            return Enumerable.Empty<string>();
        }
    }
}
