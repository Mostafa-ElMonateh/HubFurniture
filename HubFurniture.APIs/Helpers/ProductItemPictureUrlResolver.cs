using AutoMapper;
using HubFurniture.APIs.Dtos;
using HubFurniture.Core.Entities;
using Microsoft.IdentityModel.Tokens;

namespace HubFurniture.APIs.Helpers
{
    public class ProductItemPictureUrlResolver : IValueResolver<CategoryItem, ProductItemToReturnDto, IEnumerable<string>>
    {
        private readonly IConfiguration _configuration;

        public ProductItemPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public IEnumerable<string> Resolve(CategoryItem source, ProductItemToReturnDto destination, IEnumerable<string> destMember,
            ResolutionContext context)
        {
            if (source.ProductPictures.Any())
            {
                return source.ProductPictures.Select(pp => $"{_configuration["ApiBaseUrl"]}/{pp.PictureUrl}");
            }

            return Enumerable.Empty<string>();
        }
    }
}
