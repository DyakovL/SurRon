using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using SurRon.Infrastructure.Data.Models;
using SurRon.Models.MotorcycleTypes;
using static SurRon.Infrastructure.DataConstants.ErrorMessages;
using static SurRon.Infrastructure.DataConstants.MotorcycleConstants;

namespace SurRon.Models.Motorcycles
{
    public class MotorcycleFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(VinLength, MinimumLength = VinLength, ErrorMessage = LengthDataError)]
        public string Vin { get; set; } = string.Empty;

        [Required]
        [StringLength(EngineMaxLength, MinimumLength = EngineMinLength, ErrorMessage = LengthDataError)]
        public string Engine { get; set; } = string.Empty;

        [Required]
        [StringLength(ColorMaxLength, MinimumLength = ColorMinLength, ErrorMessage = LengthDataError)]
        public string Color { get; set; } = string.Empty;

        [Required]
        [DisplayName("Motorcycle Type")]
        public int MotorcycleTypeId { get; set; }


        public IEnumerable<MotorcycleTypeView> MotorcycleType { get; set; }
            = new List<MotorcycleTypeView>();
    }
}
