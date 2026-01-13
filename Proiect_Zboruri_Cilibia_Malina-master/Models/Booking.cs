using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proiect_Zboruri_Cilibia_Malina.Models
{
    public class Booking
    {
        public int ID { get; set; }

        [Range(1, 10000)] 
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }


        public int FlightID { get; set; }
        public Flight? Flight { get; set; }

        public int PassengerID { get; set; }
        public Passenger? Passenger { get; set; }

        [Display(Name = "Ticket Class")]
        public int TicketClassID { get; set; }
        public TicketClass? TicketClass
        {
            get; set;
        }
    }
    }
