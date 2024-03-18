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

            CreateMap<Category, ProductCategoryToReturnDto>().ForMember(d => d.CategorySets,
                o => o.MapFrom<ProductCategoryResolver>());

            CreateMap<RegisterUserDto, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.LastName + src.FirstName))
            .ReverseMap();

        }
    }
}
