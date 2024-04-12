using System.ComponentModel.DataAnnotations;
using static SurRon.Infrastructure.DataConstants.InventoryConstants;
using static SurRon.Infrastructure.DataConstants.ErrorMessages;

namespace SurRon.Models.Inventory
{
    public class InventoryTypeView
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength,
            MinimumLength = NameMinLength,
            ErrorMessage = LengthDataError)]
        public string Name { get; set; } = string.Empty;
    }
}
