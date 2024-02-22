 namespace SurRon.Data.Models
{
    public class Events
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public string Location { get; set; } = string.Empty;

        public User User { get; set; } = null!;
    }
}
