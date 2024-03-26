using HubFurniture.Core.Entities;
using HubFurniture.Core.Enums;

namespace HubFurniture.APIs.Dtos
{
    public class ProductSetToReturnDto : ProductToReturnDto
    {
        public ICollection<SetItemToReturnDto> Items { get; set; } = new HashSet<SetItemToReturnDto>();
    }
}
