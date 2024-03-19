using HubFurniture.Core.Enums;

namespace HubFurniture.APIs.Dtos
{
    public class ProductSetToReturnDto : ProductToReturnDto
    {
        
        public IEnumerable<SetItemToReturnDto> CategoryItems { get; set; } = new HashSet<SetItemToReturnDto>();
    }
}
