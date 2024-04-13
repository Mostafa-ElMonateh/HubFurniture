namespace AdminPanel.Models
{
    public class SetItemViewModel
    {
        public int Id { get; set; }

        public string NameEnglish { get; set; }
        public string NameArabic { get; set; }
        public decimal? Height { get; set; }
        public decimal? Width { get; set; }
        public decimal? Depth { get; set; }
        public int CategorySetId { get; set; }
    }
}
