namespace HubFurniture.APIs.Dtos
{
    public class OrderEnToReturnDto : OrderToReturnDto
    {
        public ICollection<OrderItemEnToReturnDto> OrderItems { get; set; } = new HashSet<OrderItemEnToReturnDto>();
    }
}
