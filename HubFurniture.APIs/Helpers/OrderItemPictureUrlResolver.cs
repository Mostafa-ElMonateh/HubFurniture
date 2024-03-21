using AutoMapper;
using HubFurniture.APIs.Dtos;
using HubFurniture.Core.Entities.Order_Aggregate;

namespace HubFurniture.APIs.Helpers
{
    public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ProductOrdered.PictureUrl))
            {
                return $"{_configuration["ApiBaseUrl"]}/{source.ProductOrdered.PictureUrl}";
            }

            return string.Empty;
        }
    }
}
