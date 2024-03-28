using AutoMapper;
using HubFurniture.APIs.Dtos;
using HubFurniture.Core.Entities;
using System.Globalization;

namespace HubFurniture.APIs.Helpers
{
    public class ProductCategorySetsResolver : IValueResolver<Category, ProductCategoryToReturnDto, IEnumerable<CategoryTypesToReturnDto>>
    {
        public IEnumerable<CategoryTypesToReturnDto> Resolve(Category source, ProductCategoryToReturnDto destination, IEnumerable<CategoryTypesToReturnDto> destMember,
            ResolutionContext context)
        {
            if (source.CategorySetsTypes.Any())
            {
                var currentCulture = CultureInfo.CurrentCulture.Name;

                return source.CategorySetsTypes.Select(cst =>
                {
                    var name = currentCulture.StartsWith("ar") ? cst.NameArabic : cst.NameEnglish;
                    return new CategoryTypesToReturnDto { Id = cst.Id, Name = name };
                });
            }

            return Enumerable.Empty<CategoryTypesToReturnDto>();
        }

    }
}
