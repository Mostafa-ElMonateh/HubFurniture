using AutoMapper;
using HubFurniture.APIs.Dtos;
using HubFurniture.Core.Entities;

namespace HubFurniture.APIs.Helpers
{
    public class ProductSetPictureUrlResolver : IValueResolver<CategorySet, ProductSetToReturnDto, IEnumerable<string>>
    {
        private readonly IConfiguration _configuration;

        public ProductSetPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<string> Resolve(CategorySet source, ProductSetToReturnDto destination, IEnumerable<string> destMember,
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
