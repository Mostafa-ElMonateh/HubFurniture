using HubFurniture.Core.Entities;
using HubFurniture.Core.Enums;

namespace HubFurniture.APIs.Dtos
{
    public class ProductSetToReturnDto : ProductToReturnDto
    {
        public List<SetItemToReturnDto> Items { get; set; } = new List<SetItemToReturnDto>();
    }
}
