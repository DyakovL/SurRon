using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SurRon.Infrastructure.Data.Models;
using SurRon.Models.MotorcycleTypes;

namespace SurRon.Models.Motorcycles
{
    public class MotorcycleFormViewModel
    {
        public int Id { get; set; }

        public string Vin { get; set; } = string.Empty;

        public string Engine { get; set; } = string.Empty;

        public string Color { get; set; } = string.Empty;

        [Required]
        [DisplayName("Motorcycle Type")]
        public int MotorcycleTypeId { get; set; }


        public IEnumerable<MotorcycleTypeView> MotorcycleType { get; set; }
            = new List<MotorcycleTypeView>();
    }
}
