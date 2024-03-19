namespace HubFurniture.APIs.Dtos
{
    public class CategorySetsToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<CategoryTypesToReturnDto> CategorySetsTypes { get; set; }
    }
}
