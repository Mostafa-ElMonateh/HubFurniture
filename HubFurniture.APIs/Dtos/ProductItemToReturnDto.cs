using HubFurniture.Core.Enums;

namespace HubFurniture.APIs.Dtos
{
    public class ProductItemToReturnDto : ProductToReturnDto
    {
        public decimal? Height { get; set; }
        public decimal? Depth { get; set; }
        public decimal? Width { get; set; }
    }
}
