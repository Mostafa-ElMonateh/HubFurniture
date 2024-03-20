using System.ComponentModel.DataAnnotations;
using HubFurniture.Core.Entities;

namespace HubFurniture.APIs.Dtos
{
    public class CustomerBasketDto
    {
        [Required]
        public string BasketId { get; set; }
        public List<BasketItemDto> BasketItems{ get; set; }
    }
}
