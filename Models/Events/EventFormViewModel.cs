using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic.CompilerServices;
using static SurRon.Infrastructure.DataConstants.EventConstants;
using static SurRon.Infrastructure.DataConstants.ErrorMessages;

namespace SurRon.Models.Events
{
    public class EventFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength,
            MinimumLength = NameMinLength,
            ErrorMessage = LengthDataError)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(DescriptionMaxLength,
            MinimumLength = DescriptionMinLength,
            ErrorMessage = LengthDataError)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(LocationMaxLength,
            MinimumLength = LocationMinLength,
            ErrorMessage = LengthDataError)]
        public string Location { get; set; } = string.Empty;

    }
}
