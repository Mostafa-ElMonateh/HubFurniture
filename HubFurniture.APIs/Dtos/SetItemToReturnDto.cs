namespace HubFurniture.APIs.Dtos
{
    public class SetItemToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Height { get; set; }
        public decimal? Depth { get; set; }
        public decimal? Width { get; set; }
    }
}
