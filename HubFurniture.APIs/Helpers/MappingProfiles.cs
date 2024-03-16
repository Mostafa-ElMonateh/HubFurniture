using AutoMapper;
using HubFurniture.APIs.Dtos;
using HubFurniture.Core.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HubFurniture.APIs.Helpers
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {

            CreateMap<CustomerReview, CustomerReviewToReturnDto>();
            CreateMap<ProductItem, ProductItemToReturnDto>().ForMember(d => d.ProductPictures, 
                o => o.MapFrom<ProductPictureUrlResolver>());
        }
    }
}
