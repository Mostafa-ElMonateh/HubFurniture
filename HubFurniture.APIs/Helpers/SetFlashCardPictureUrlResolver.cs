using AutoMapper;
using HubFurniture.APIs.Dtos;
using HubFurniture.Core.Entities;

namespace HubFurniture.APIs.Helpers
{
    public class SetFlashCardPictureUrlResolver : IValueResolver<CategorySet, SetFlashCardToReturnDto, IEnumerable<string>>
    {
        private readonly IConfiguration _configuration;

        public SetFlashCardPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public IEnumerable<string> Resolve(CategorySet source, SetFlashCardToReturnDto destination, IEnumerable<string> destMember,
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
