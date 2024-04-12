using SurRon.Infrastructure.Data.Models;
using SurRon.Models.Inventory;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;


namespace SurRon.Models.SoldMotorcycles
{
    public class SellItemFormViewModel
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime SoldOn { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Amount { get; set; }
        
        public int InventoryItemId { get; set; }

        public IEnumerable<InventoryTypeView> InventoryItems { get; set; } 
            = new List<InventoryTypeView>();
    }
}
