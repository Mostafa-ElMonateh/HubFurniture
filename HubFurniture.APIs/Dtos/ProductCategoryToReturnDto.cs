namespace HubFurniture.APIs.Dtos
{
    public class ProductCategoryToReturnDto
    {
        public string Name { get; set; }
        public IEnumerable<string> CategorySets { get; set; }
    }
}
