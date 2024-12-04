using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SportFacilitiesManagement.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Display(Name="Customer")]
        [Required]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [ValidateNever]
        public Customer Customer { get; set; }

        [Required]
        
        [ForeignKey("Facility")]
        public int FacilityId { get; set; }

        [ValidateNever]
        public Facility Facility { get; set; }

        [Required]

        [Display(Name = "Reservation Date")]
        [DataType(dataType: DataType.Date)]
        public DateTime ReservationDate { get; set; }

        [Display(Name = "Reservation Hours")]
        public int ReservationHours { get; set; }

        [Display(Name = "Total")]
        public double TotalPrice { get; set; }


    }
}
