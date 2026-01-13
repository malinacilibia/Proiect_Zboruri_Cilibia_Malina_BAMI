using System.ComponentModel.DataAnnotations;

namespace Proiect_Zboruri_Cilibia_Malina.Models
{
    public class Airline
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter the airline name.")] 
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Company Name")]
        public string Name { get; set; }

        public ICollection<Flight>? Flights { get; set; }

    }
}
