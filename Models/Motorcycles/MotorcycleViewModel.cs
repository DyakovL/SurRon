using SurRon.Data.Models;
using SurRon.Infrastructure.DataConstants;

namespace SurRon.Models.Motorcycles
{
    public class MotorcycleViewModel
    {

        public MotorcycleViewModel(
            int id,
            string vin,
            string color,
            string engine,
            string motorcycleTypes,
            string uploader)
        {
            Id = id;
            Vin = vin;
            Color = color;
            Engine = engine;
            MotorcycleType = motorcycleTypes;
            Uploader = uploader;
        }

        public int Id { get; set; }

        public string Vin { get; set; }

        public string Color { get; set; }

        public string Engine { get; set; }

        public string MotorcycleType { get; set; }

        public string Uploader { get; set; }
    }
}
