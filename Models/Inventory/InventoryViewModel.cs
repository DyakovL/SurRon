namespace SurRon.Models.Inventory
{
    public class InventoryViewModel
    {

        public InventoryViewModel(int id, string name, decimal price, int amount, string imageUrl)
        {
            Id = id;
            Name = name;
            Price = price;
            Amount = amount;
            ImageUrl = imageUrl;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }

        public string ImageUrl { get; set; }
    }
}
