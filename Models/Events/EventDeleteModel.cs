namespace SurRon.Models.Events
{
    public class EventDeleteModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; } = string.Empty;

        public DateTime Date { get; set; }

        public string Organizer { get; set; } = string.Empty;
    }
}
