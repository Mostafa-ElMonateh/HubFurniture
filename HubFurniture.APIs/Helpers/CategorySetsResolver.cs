using AutoMapper;
using HubFurniture.APIs.Dtos;
using HubFurniture.Core.Entities;
using System.Globalization;

namespace HubFurniture.APIs.Helpers
{
    public class CategorySetsResolver : IValueResolver<Category, CategorySetsToReturnDto, IEnumerable<CategoryTypesToReturnDto>>
    {
        public IEnumerable<CategoryTypesToReturnDto> Resolve(Category source, CategorySetsToReturnDto destination, IEnumerable<CategoryTypesToReturnDto> destMember,
            ResolutionContext context)
        {
            var currentCulture = CultureInfo.CurrentCulture.Name;

            if (source.CategorySetsTypes.Any())
            {
                return source.CategorySetsTypes.Select(
                    cst =>
                    {
                        var name = currentCulture.StartsWith("ar") ? cst.NameArabic : cst.NameEnglish;
                        return new CategoryTypesToReturnDto { Id = cst.Id, Name = name };
                    });
            }

            return Enumerable.Empty<CategoryTypesToReturnDto>();
        }
    }
}
