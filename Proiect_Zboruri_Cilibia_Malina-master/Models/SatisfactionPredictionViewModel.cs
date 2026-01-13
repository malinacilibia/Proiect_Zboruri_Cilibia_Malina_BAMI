using System.ComponentModel.DataAnnotations;

namespace Proiect_Zboruri_Cilibia_Malina.Models
{
    public class SatisfactionPredictionViewModel
    {
        // --- Date Demografice și Zbor ---

        [Display(Name = "Gender (Male/Female)")]
        public string Gender { get; set; }

        [Display(Name = "Customer Type (Loyal Customer/disloyal Customer)")]
        public string CustomerType { get; set; }

        [Display(Name = "Age")]
        public float Age { get; set; }

        [Display(Name = "Type of Travel (Business travel/Personal Travel)")]
        public string TypeOfTravel { get; set; }

        [Display(Name = "Class (Business/Eco/Eco Plus)")]
        public string Class { get; set; }

        [Display(Name = "Flight Distance")]
        public float FlightDistance { get; set; }

        // --- Servicii (Note 0-5) ---

        [Display(Name = "Inflight Wifi Service (0-5)")]
        public float InflightWifiService { get; set; }

        [Display(Name = "Departure/Arrival Time Convenient (0-5)")]
        public float DepartureArrivalTimeConvenient { get; set; }

        [Display(Name = "Ease of Online Booking (0-5)")]
        public float EaseOfOnlineBooking { get; set; }

        [Display(Name = "Gate Location (0-5)")]
        public float GateLocation { get; set; }

        [Display(Name = "Food and Drink (0-5)")]
        public float FoodAndDrink { get; set; }

        [Display(Name = "Online Boarding (0-5)")]
        public float OnlineBoarding { get; set; }

        [Display(Name = "Seat Comfort (0-5)")]
        public float SeatComfort { get; set; }

        [Display(Name = "Inflight Entertainment (0-5)")]
        public float InflightEntertainment { get; set; }

        [Display(Name = "On-board Service (0-5)")]
        public float OnBoardService { get; set; }

        [Display(Name = "Leg Room Service (0-5)")]
        public float LegRoomService { get; set; }

        [Display(Name = "Baggage Handling (0-5)")]
        public float BaggageHandling { get; set; }

        [Display(Name = "Check-in Service (0-5)")]
        public float CheckinService { get; set; }

        [Display(Name = "Inflight Service (0-5)")]
        public float InflightService { get; set; }

        [Display(Name = "Cleanliness (0-5)")]
        public float Cleanliness { get; set; }

        // --- Întârzieri ---

        [Display(Name = "Departure Delay (Minutes)")]
        public float DepartureDelayInMinutes { get; set; }

        [Display(Name = "Arrival Delay (Minutes)")]
        public float ArrivalDelayInMinutes { get; set; }

        // --- REZULTATUL ÎNTORS DE API ---
        [Display(Name = "Predicted Satisfaction")]
        public string? PredictedSatisfaction { get; set; }
    }
}