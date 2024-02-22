namespace SurRon.Data.Models
{
    public class Seller
    {
        public int Id { get; set; }

        public IEnumerable<Motorcycle> Motorcycles { get; set; } = null!;

        public IList<User> Users { get; set; } 
            = new List<User>();
    }
}
