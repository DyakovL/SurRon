namespace SurRon.Models.Inventory
{
    public class InventoryViewModel
    {

        public InventoryViewModel(int id, string name, decimal price, int amount)
        {
            Id = id;
            Name = name;
            Price = price;
            Amount = amount;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }
    }
}
