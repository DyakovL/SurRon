using SurRon.Infrastructure.Data.Models;
using SurRon.Models.Inventory;
using System.ComponentModel.DataAnnotations;


namespace SurRon.Models.SoldMotorcycles
{
    public class SellItemFormViewModel
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime SoldOn { get; set; }


        public decimal Price { get; set; }

        public int Amount { get; set; }
        
        public int InventoryItemId { get; set; }

        public IEnumerable<InventoryTypeView> InventoryItems { get; set; } 
            = new List<InventoryTypeView>();
    }
}
