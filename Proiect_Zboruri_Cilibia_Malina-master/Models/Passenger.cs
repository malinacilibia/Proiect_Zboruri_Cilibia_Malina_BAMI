using System.ComponentModel.DataAnnotations;


namespace Proiect_Zboruri_Cilibia_Malina.Models
{
    public class Passenger
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email address")]
        public string Email { get; set; }
        public ICollection<Booking>? Bookings { get; set; }

    }
}
