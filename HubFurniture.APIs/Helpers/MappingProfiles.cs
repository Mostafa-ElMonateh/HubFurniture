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

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();

            CreateMap<CustomerReview, CustomerReviewToReturnDto>();

            CreateMap<CategoryItem, ProductItemToReturnDto>().ForMember(d => d.ProductPictures, 
                o => o.MapFrom<ProductItemPictureUrlResolver>());

            CreateMap<CategoryItem, productFlashCardToReturnDto>().ForMember(d => d.Availability,
                o => o.MapFrom(s => s.Availability.ToString()))
                .ForMember(d => d.ProductPictures, 
                o => o.MapFrom<ProductItemFlashCardPictureUrlResolver>());

            CreateMap<CategorySet, ProductSetToReturnDto>().ForMember(d => d.ProductPictures, 
                o => o.MapFrom<ProductSetPictureUrlResolver>())
                .ForMember(d => d.Availability, o => o.MapFrom(s => s.Availability.ToString()))
                .ForMember(d => d.Suitability, o => o.MapFrom(s => s.Suitability.ToString()));

            CreateMap<CategorySet, productFlashCardToReturnDto>().ForMember(d => d.Availability,
                    o => o.MapFrom(s => s.Availability.ToString()))
                .ForMember(d => d.ProductPictures, 
                    o => o.MapFrom<ProductSetFlashCardPictureUrlResolver>());

            CreateMap<Category, ProductCategoryToReturnDto>().ForMember(d => d.CategorySetsTypes,
                o => o.MapFrom<ProductCategorySetsResolver>())
                .ForMember(d => d.CategoryItemsTypes, o=>o.MapFrom<ProductCategoryItemsResolver>());

            CreateMap<CategoryItem, ProductItemToReturnDto>().ForMember(d => d.ProductPictures, 
                    o => o.MapFrom<ProductItemPictureUrlResolver>())
                .ForMember(d => d.Availability, o => o.MapFrom(s => s.Availability.ToString()))
                .ForMember(d => d.Suitability, o => o.MapFrom(s => s.Suitability.ToString()));

            CreateMap<CategoryItem, SetItemToReturnDto>();

            CreateMap<Category, CategorySetsToReturnDto>().ForMember(d => d.CategorySetsTypes,
                    o => o.MapFrom<CategorySetsResolver>());

            CreateMap<Category, CategoryItemsToReturn>().ForMember(d => d.CategoryItemsTypes,
                o => o.MapFrom<CategoryItemsResolver>());


            CreateMap<RegisterUserDto, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.LastName + src.FirstName))
            .ReverseMap();

        }
    }
}
