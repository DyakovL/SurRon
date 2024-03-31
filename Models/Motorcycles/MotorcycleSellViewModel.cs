using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SurRon.Infrastructure.Data.Models;
using SurRon.Models.MotorcycleTypes;

namespace SurRon.Models.Motorcycles
{
    public class MotorcycleSellViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }

        public string Address { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string Vin { get; set; } = string.Empty;

        public string Engine { get; set; } = string.Empty;

        public string Color { get; set; } = string.Empty;

        public string MotorcycleType { get; set; } = string.Empty;

        public bool Warranty { get; set; }

    }
}
