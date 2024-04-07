using AdminPanel.Models;
using AutoMapper;
using HubFurniture.Core.Entities;
using HubFurniture.Core.Entities.Order_Aggregate;

namespace AdminPanel.Helpers
{
    public class MapsProfile : Profile
    {
        public MapsProfile()
        {
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<CategoryItem, ItemViewModel>().ReverseMap();
            CreateMap<CategoryItemType, ItemTypeToReturnViewModel>().ReverseMap();
            CreateMap<Category, ItemCategoryViewModel>().ReverseMap();
            CreateMap<CategoryItemType, ItemsTypesInCategoryViewModel>();
            CreateMap<CategoryItemType, ItemTypeViewModel>().ReverseMap();
            CreateMap<DeliveryMethod, DeliveryMethodViewModel>().ReverseMap();
        }
    }
}
