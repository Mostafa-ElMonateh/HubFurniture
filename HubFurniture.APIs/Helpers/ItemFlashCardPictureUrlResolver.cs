using AutoMapper;
using HubFurniture.APIs.Dtos;
using HubFurniture.Core.Entities;

namespace HubFurniture.APIs.Helpers
{
    public class ItemFlashCardPictureUrlResolver : IValueResolver<CategoryItem, ItemFlashCardToReturnDto, IEnumerable<string>>
    {
        private readonly IConfiguration _configuration;

        public ItemFlashCardPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public IEnumerable<string> Resolve(CategoryItem source, ItemFlashCardToReturnDto destination, IEnumerable<string> destMember,
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
