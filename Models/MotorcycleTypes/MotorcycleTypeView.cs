using System.ComponentModel.DataAnnotations;

namespace SurRon.Models.MotorcycleTypes
{
    public class MotorcycleTypeView
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
