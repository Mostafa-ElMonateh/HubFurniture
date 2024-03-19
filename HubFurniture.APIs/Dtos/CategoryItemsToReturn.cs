namespace HubFurniture.APIs.Dtos
{
    public class CategoryItemsToReturn
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<CategoryTypesToReturnDto> CategoryItemsTypes { get; set; }
    }
}
