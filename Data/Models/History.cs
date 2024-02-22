namespace SurRon.Data.Models
{
    public class History
    {
        public int Id { get; set; }

        public DateTime DateOfService { get; set; }

        public string ItemChanged { get; set; } = string.Empty;

        public decimal Price { get; set; }
    }
}
