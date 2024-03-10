using System.Collections;
using static SurRon.Infrastructure.DataConstants.EventConstants;

namespace SurRon.Models.Events
{
    public class EventViewModel
    {
        public EventViewModel(
            int id, 
            string name, 
            string description, 
            DateTime date, 
            string location, 
            string organizer)
        {
            Id = id;
            Name = name;
            Description = description;
            Date = date.ToString(DateFormat);
            Location = location;
            Organizer = organizer;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Date { get; set; }

        public string Location { get; set; }

        public string Organizer { get; set; }
    }
}
