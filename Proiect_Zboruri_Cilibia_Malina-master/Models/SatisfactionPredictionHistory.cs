using System;
using System.ComponentModel.DataAnnotations;

namespace Proiect_Zboruri_Cilibia_Malina.Models
{
    public class SatisfactionPredictionHistory
    {
        [Key]
        public int ID { get; set; }

        // --- Date Demografice & Zbor ---
        [Display(Name = "Gen")]
        public string Gender { get; set; }

        [Display(Name = "Tip Client")]
        public string CustomerType { get; set; }

        [Display(Name = "Vârstă")]
        public float Age { get; set; }

        [Display(Name = "Tip Călătorie")]
        public string TypeOfTravel { get; set; }

        [Display(Name = "Clasă")]
        public string Class { get; set; }

        [Display(Name = "Distanță Zbor")]
        public float FlightDistance { get; set; }

        // --- Servicii (Note) ---
        [Display(Name = "Wifi")]
        public float InflightWifiService { get; set; }

        [Display(Name = "Plecare/Sosire Convenabilă")]
        public float DepartureArrivalTimeConvenient { get; set; }

        [Display(Name = "Booking Online")]
        public float EaseOfOnlineBooking { get; set; }

        [Display(Name = "Locație Poartă")]
        public float GateLocation { get; set; }

        [Display(Name = "Mâncare & Băutură")]
        public float FoodAndDrink { get; set; }

        [Display(Name = "Boarding Online")]
        public float OnlineBoarding { get; set; }

        [Display(Name = "Confort Scaun")]
        public float SeatComfort { get; set; }

        [Display(Name = "Divertisment")]
        public float InflightEntertainment { get; set; }

        [Display(Name = "Serviciu la Bord")]
        public float OnBoardService { get; set; }

        [Display(Name = "Spațiu Picioare")]
        public float LegRoomService { get; set; }

        [Display(Name = "Manipulare Bagaje")]
        public float BaggageHandling { get; set; }

        [Display(Name = "Check-in")]
        public float CheckinService { get; set; }

        [Display(Name = "Serviciu Zbor")]
        public float InflightService { get; set; }

        [Display(Name = "Curățenie")]
        public float Cleanliness { get; set; }

        // --- Întârzieri ---
        [Display(Name = "Întârziere Plecare (min)")]
        public float DepartureDelayInMinutes { get; set; }

        [Display(Name = "Întârziere Sosire (min)")]
        public float ArrivalDelayInMinutes { get; set; }

        // --- Rezultat ---
        [Display(Name = "Predicție")]
        public string PredictedSatisfaction { get; set; }

        [Display(Name = "Data")]
        public DateTime CreatedAt { get; set; }
    }
}