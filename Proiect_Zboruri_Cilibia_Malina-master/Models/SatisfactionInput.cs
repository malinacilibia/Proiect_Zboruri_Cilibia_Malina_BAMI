using System.Text.Json.Serialization;

namespace Proiect_Zboruri_Cilibia_Malina.Models
{
    public class SatisfactionInput
    {
        [JsonPropertyName("column1")]
        public float Column1 { get; set; }

        [JsonPropertyName("id")]
        public float Id { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; } 

        [JsonPropertyName("customer_Type")]
        public string CustomerType { get; set; }

        [JsonPropertyName("age")]
        public float Age { get; set; }

        [JsonPropertyName("type_of_Travel")]
        public string TypeOfTravel { get; set; }

        [JsonPropertyName("class")]
        public string Class { get; set; } 

        [JsonPropertyName("flight_Distance")]
        public float FlightDistance { get; set; }

        [JsonPropertyName("inflight_wifi_service")]
        public float InflightWifiService { get; set; }

        [JsonPropertyName("departure_Arrival_time_convenient")]
        public float DepartureArrivalTimeConvenient { get; set; }

        [JsonPropertyName("ease_of_Online_booking")]
        public float EaseOfOnlineBooking { get; set; }

        [JsonPropertyName("gate_location")]
        public float GateLocation { get; set; }

        [JsonPropertyName("food_and_drink")]
        public float FoodAndDrink { get; set; }

        [JsonPropertyName("online_boarding")]
        public float OnlineBoarding { get; set; }

        [JsonPropertyName("seat_comfort")]
        public float SeatComfort { get; set; }

        [JsonPropertyName("inflight_entertainment")]
        public float InflightEntertainment { get; set; }

        [JsonPropertyName("on_board_service")]
        public float OnBoardService { get; set; }

        [JsonPropertyName("leg_room_service")]
        public float LegRoomService { get; set; }

        [JsonPropertyName("baggage_handling")]
        public float BaggageHandling { get; set; }

        [JsonPropertyName("checkin_service")]
        public float CheckinService { get; set; }

        [JsonPropertyName("inflight_service")]
        public float InflightService { get; set; }

        [JsonPropertyName("cleanliness")]
        public float Cleanliness { get; set; }

        [JsonPropertyName("departure_Delay_in_Minutes")]
        public float DepartureDelayInMinutes { get; set; }

        [JsonPropertyName("arrival_Delay_in_Minutes")]
        public float ArrivalDelayInMinutes { get; set; }

        [JsonPropertyName("satisfaction")]
        public string Satisfaction { get; set; }
    }
}