namespace SurRon.Models.Events
{
    public class EventDetailsModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public string Organizer { get; set; } = null!;
    }
}
