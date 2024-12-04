using System.ComponentModel.DataAnnotations;

namespace SportFacilitiesManagement.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone { get; set; }

        public long Age { get; set; }

        [Display(Name = "Date of Birth")]
        [Required]
        [DataType(dataType: DataType.Date)]
        public DateTime DateOfBirth { get; set; }
    }
}
