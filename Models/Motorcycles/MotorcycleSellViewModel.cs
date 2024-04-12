using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SurRon.Infrastructure.Data.Models;
using SurRon.Models.MotorcycleTypes;
using static SurRon.Infrastructure.DataConstants.ErrorMessages;
using static SurRon.Infrastructure.DataConstants.MotorcycleConstants;

namespace SurRon.Models.Motorcycles
{
    public class MotorcycleSellViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = LengthDataError)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength, ErrorMessage = LengthDataError)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [StringLength(CityMaxLength, MinimumLength = CityMinLength, ErrorMessage = LengthDataError)]
        public string City { get; set; } = string.Empty;

        [Required]
        [StringLength(CountryMaxLength, MinimumLength = CountryMinLength, ErrorMessage = LengthDataError)]
        public string Country { get; set; } = string.Empty;

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
        public string MotorcycleType { get; set; } = string.Empty;

        public bool Warranty { get; set; }

    }
}
