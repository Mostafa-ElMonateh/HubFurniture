using AutoMapper;
using HubFurniture.APIs.Dtos;
using HubFurniture.Core.Entities;

namespace HubFurniture.APIs.Helpers
{
    public class ProductSetFlashCardPictureUrlResolver : IValueResolver<CategorySet, productFlashCardToReturnDto, IEnumerable<string>>
    {
        private readonly IConfiguration _configuration;

        public ProductSetFlashCardPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public IEnumerable<string> Resolve(CategorySet source, productFlashCardToReturnDto destination, IEnumerable<string> destMember,
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
