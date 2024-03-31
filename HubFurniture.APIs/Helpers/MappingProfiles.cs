using AutoMapper;
using HubFurniture.APIs.Dtos;
using HubFurniture.Core.Entities;
using HubFurniture.Core.Entities.Order_Aggregate;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HubFurniture.APIs.Helpers
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, 
                    o=>
                        o.MapFrom(s => s.DeliveryMethod.Name))
                .ForMember(d => d.DeliveryMethodCost,
                    o =>
                        o.MapFrom(s => s.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId,
                    o =>
                        o.MapFrom(s => s.ProductOrdered.ProductId))
                .ForMember(d => d.ProductNameArabic,
                    o =>
                        o.MapFrom(s => s.ProductOrdered.ProductNameArabic))
                .ForMember(d => d.ProductNameEnglish,
                    o =>
                        o.MapFrom(s => s.ProductOrdered.ProductNameEnglish))
                .ForMember(d => d.PictureUrl,
                    o =>
                        o.MapFrom<OrderItemPictureUrlResolver>())
                .ForMember(d => d.Type,
                    o =>
                        o.MapFrom(s => s.ProductOrdered.Type));

            CreateMap<AddressDto, HubFurniture.Core.Entities.Order_Aggregate.Address>();

            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();

            CreateMap<CustomerReview, CustomerReviewToReturnDto>();

            CreateMap<CategoryItem, ProductItemToReturnDto>().ForMember(d => d.ProductPictures, 
                o => o.MapFrom<ProductItemPictureUrlResolver>());

            CreateMap<CategoryItem, ItemFlashCardToReturnDto>().ForMember(d => d.Availability,
                o => o.MapFrom(s => s.Availability.ToString()))
                .ForMember(d => d.ProductPictures, 
                o => o.MapFrom<ItemFlashCardPictureUrlResolver>());

            CreateMap<CategorySet, ProductSetToReturnDto>().ForMember(d => d.ProductPictures, 
                o => o.MapFrom<ProductSetPictureUrlResolver>())
                .ForMember(d => d.Availability, o => o.MapFrom(s => s.Availability.ToString()))
                .ForMember(d => d.Suitability, o => o.MapFrom(s => s.Suitability.ToString()));

            CreateMap<CategorySet, SetFlashCardToReturnDto>().ForMember(d => d.Availability,
                    o => o.MapFrom(s => s.Availability.ToString()))
                .ForMember(d => d.ProductPictures, 
                    o => o.MapFrom<SetFlashCardPictureUrlResolver>());

            CreateMap<Category, ProductCategoryToReturnDto>().ForMember(d => d.CategorySetsTypes,
                o => o.MapFrom<ProductCategorySetsResolver>())
                .ForMember(d => d.CategoryItemsTypes, o=>o.MapFrom<ProductCategoryItemsResolver>());

            CreateMap<CategoryItem, ProductItemToReturnDto>().ForMember(d => d.ProductPictures, 
                    o => o.MapFrom<ProductItemPictureUrlResolver>())
                .ForMember(d => d.Availability, o => o.MapFrom(s => s.Availability.ToString()))
                .ForMember(d => d.Suitability, o => o.MapFrom(s => s.Suitability.ToString()));

            CreateMap<SetItem, SetItemToReturnDto>();

            CreateMap<Category, CategorySetsToReturnDto>().ForMember(d => d.CategorySetsTypes,
                    o => o.MapFrom<CategorySetsResolver>());

            CreateMap<Category, CategoryItemsToReturn>().ForMember(d => d.CategoryItemsTypes,
                o => o.MapFrom<CategoryItemsResolver>());


            CreateMap<RegisterUserDto, ApplicationUser>()
            .ReverseMap();

            CreateMap<UserAddressDto, UserAddress>()
                .ReverseMap();

        }
    }
}
