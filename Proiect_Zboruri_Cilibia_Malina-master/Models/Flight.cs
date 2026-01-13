using System.ComponentModel.DataAnnotations;

namespace Proiect_Zboruri_Cilibia_Malina.Models
{
    public class Flight
    {
        public int ID { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Flight Number")]
        public string FlightNumber { get; set; } 



        [DataType(DataType.DateTime)]
        public DateTime DepartureTime { get; set; }


        [Range(0.5, 100.0, ErrorMessage = "Duration must be between 30 min and 100 hours")]
        [Display(Name = "Duration (hours)")]
        public double Duration { get; set; }

        [Display(Name = "Destination")]
        public string Destination { get; set; }

        public int AirlineID { get; set; } 
        public Airline? Airline { get; set; }

        public int AirportID { get; set; } 
        public Airport? Airport { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}
