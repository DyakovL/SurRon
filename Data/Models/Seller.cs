namespace SurRon.Data.Models
{
    public class Seller
    {
        public int Id { get; set; }

        public IEnumerable<Motorcycle> Motorcycles { get; set; }

        public IList<Client> Clients { get; set; } 
            = new List<Client>();
    }
}
