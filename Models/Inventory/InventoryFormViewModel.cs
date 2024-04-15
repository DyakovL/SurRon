using System.ComponentModel.DataAnnotations;
using static SurRon.Infrastructure.DataConstants.InventoryConstants;
using static SurRon.Infrastructure.DataConstants.ErrorMessages;

namespace SurRon.Models.Inventory
{
    public class InventoryFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(NameMaxLength,
            MinimumLength = NameMinLength,
            ErrorMessage = LengthDataError)]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
