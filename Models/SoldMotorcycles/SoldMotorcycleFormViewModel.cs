using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using SurRon.Infrastructure.Data.Models;
using SurRon.Models.MotorcycleTypes;

namespace SurRon.Models.SoldMotorcycles
{
    public class SoldMotorcycleFormViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }= string.Empty;

        public string Vin { get; set; } = string.Empty;

        public string Engine { get; set; } = string.Empty;

        public string Color { get; set; } = string.Empty;

        public int MotorcycleTypeId { get; set; }

        public DateTime DateSold { get; set; }

        public string City { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        [Required]
        public string UploaderId { get; set; } = string.Empty;

        [ForeignKey(nameof(UploaderId))]
        public IdentityUser Uploader { get; set; } = null!;

        public IEnumerable<MotorcycleTypeView> MotorcycleType { get; set; }
            = new List<MotorcycleTypeView>();
    }
}
