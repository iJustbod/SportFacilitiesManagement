using System.ComponentModel.DataAnnotations;

namespace SportFacilitiesManagement.Models
{
    public class Facility
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        public int Capacity { get; set; }

        [Display(Name="Hourly Rate")] 
        public double HourlyRate { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }


        [StringLength(500)]
        public string Description { get; set; }
    }
}
