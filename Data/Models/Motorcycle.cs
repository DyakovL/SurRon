namespace SurRon.Data.Models
{
    public class Motorcycle
    {
        public int Id { get; set; }

        public string Vin { get; set; } = string.Empty;

        public string Color { get; set; } = string.Empty;

        public ModelType Model { get; set; } 
    }
}
