using System.ComponentModel.DataAnnotations;

namespace Proiect_Zboruri_Cilibia_Malina.Models
{
    public class Airport
    {
        public int ID { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Airport Code must be exactly 3 letters.")]
        [Display(Name = "Airport Code")]
        public string Code { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }
        public ICollection<Flight>? Flights { get; set; }
    }
}
