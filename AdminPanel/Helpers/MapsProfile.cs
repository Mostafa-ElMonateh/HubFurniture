using AdminPanel.Models;
using AutoMapper;
using HubFurniture.Core.Entities;

namespace AdminPanel.Helpers
{
    public class MapsProfile : Profile
    {
        public MapsProfile()
        {
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<CategoryItem, ItemViewModel>().ReverseMap();
        }
    }
}
