namespace SurRon.Data.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public DateTime DateBought { get; set; }

        public decimal TotalMoneySpent { get; set; }

        public IEnumerable<History> History { get; set; } 
            = new List<History>();
    }
}
