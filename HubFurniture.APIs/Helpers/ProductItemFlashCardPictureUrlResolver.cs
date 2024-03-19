using AutoMapper;
using HubFurniture.APIs.Dtos;
using HubFurniture.Core.Entities;

namespace HubFurniture.APIs.Helpers
{
    public class ProductItemFlashCardPictureUrlResolver : IValueResolver<CategoryItem, productFlashCardToReturnDto, IEnumerable<string>>
    {
        private readonly IConfiguration _configuration;

        public ProductItemFlashCardPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public IEnumerable<string> Resolve(CategoryItem source, productFlashCardToReturnDto destination, IEnumerable<string> destMember,
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
