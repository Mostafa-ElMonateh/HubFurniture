namespace HubFurniture.APIs.Dtos
{
    public class OrderArToReturnDto : OrderToReturnDto
    {
        public ICollection<OrderItemArToReturnDto> OrderItems { get; set; } = new HashSet<OrderItemArToReturnDto>();
    }
}
