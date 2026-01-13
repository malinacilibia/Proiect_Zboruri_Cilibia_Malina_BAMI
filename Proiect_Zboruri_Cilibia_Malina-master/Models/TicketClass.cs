using System.ComponentModel.DataAnnotations;

namespace Proiect_Zboruri_Cilibia_Malina.Models
{
    public class TicketClass
    {
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        [Display (Name = "Class Name")]
        public string Name { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}
