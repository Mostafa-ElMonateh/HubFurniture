﻿using AutoMapper;
using HubFurniture.APIs.Dtos;
using HubFurniture.Core.Entities;
using System.Globalization;

namespace HubFurniture.APIs.Helpers
{
    public class ProductCategoryItemsResolver : IValueResolver<Category, ProductCategoryToReturnDto, IEnumerable<CategoryTypesToReturnDto>>
    {
        public IEnumerable<CategoryTypesToReturnDto> Resolve(Category source, ProductCategoryToReturnDto destination, IEnumerable<CategoryTypesToReturnDto> destMember,
            ResolutionContext context)
        {
            var currentCulture = CultureInfo.CurrentCulture.Name;

            if (source.CategoryItemsTypes.Any())
            {
                return source.CategoryItemsTypes.Select(
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
