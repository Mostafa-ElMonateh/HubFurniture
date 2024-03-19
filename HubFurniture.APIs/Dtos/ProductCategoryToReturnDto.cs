namespace HubFurniture.APIs.Dtos
{
    public class ProductCategoryToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<CategoryTypesToReturnDto> CategorySetsTypes { get; set; }
        public IEnumerable<CategoryTypesToReturnDto> CategoryItemsTypes { get; set; }
    }
}
