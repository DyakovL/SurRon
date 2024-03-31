namespace SurRon.Models.SoldMotorcycles
{
    public class SoldMotorcyclesViewModel
    {
        public SoldMotorcyclesViewModel(
            int id,
            string name,
            string address,
            string city,
            string country,
            DateTime date,
            string vin,
            string color,
            string engine,
            string motorcycleTypes,
            string uploader,
            bool warranty)
        {
            Id = id;
            Name = name;
            Address = address;
            City = city;
            Country = country;
            DateSold = date.ToString();
            Vin = vin;
            Color = color;
            Engine = engine;
            MotorcycleType = motorcycleTypes;
            Uploader = uploader;
            Warranty = warranty;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string DateSold { get; set; }


        public string Vin { get; set; }

        public string Color { get; set; }

        public string Engine { get; set; }

        public string MotorcycleType { get; set; }

        public string Uploader { get; set; }

        public bool Warranty { get; set; }
    }
}
