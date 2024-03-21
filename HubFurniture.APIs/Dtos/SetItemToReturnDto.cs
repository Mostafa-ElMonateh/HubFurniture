namespace HubFurniture.APIs.Dtos
{
    public class SetItemToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } = "item";
        public decimal? Height { get; set; }
        public decimal? Depth { get; set; }
        public decimal? Width { get; set; }
    }
}
