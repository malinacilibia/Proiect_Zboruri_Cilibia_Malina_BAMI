using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proiect_Zboruri_Cilibia_Malina.Data;
using Proiect_Zboruri_Cilibia_Malina.Models;
using Proiect_Zboruri_Cilibia_Malina.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect_Zboruri_Cilibia_Malina.Controllers
{
    public class SatisfactionPredictionController : Controller
    {
        private readonly ISatisfactionPredictionService _satisfactionService;
        private readonly FlightContext _context; // Baza de date

        public SatisfactionPredictionController(ISatisfactionPredictionService satisfactionService, FlightContext context)
        {
            _satisfactionService = satisfactionService;
            _context = context;
        }

        // GET: /SatisfactionPrediction/Index
        [HttpGet]
        public IActionResult Index()
        {
            return View(new SatisfactionPredictionViewModel());
        }

        // POST: /SatisfactionPrediction/Index
        [HttpPost]
        public async Task<IActionResult> Index(SatisfactionPredictionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // 1. Pregătim datele pentru API
                var input = new SatisfactionInput
                {
                    Gender = model.Gender,
                    CustomerType = model.CustomerType,
                    Age = model.Age,
                    TypeOfTravel = model.TypeOfTravel,
                    Class = model.Class,
                    FlightDistance = model.FlightDistance,

                    SeatComfort = model.SeatComfort,
                    InflightWifiService = model.InflightWifiService,
                    FoodAndDrink = model.FoodAndDrink,
                    InflightEntertainment = model.InflightEntertainment,
                    OnlineBoarding = model.OnlineBoarding,
                    LegRoomService = model.LegRoomService,
                    OnBoardService = model.OnBoardService,
                    CheckinService = model.CheckinService,
                    InflightService = model.InflightService,
                    Cleanliness = model.Cleanliness,
                    GateLocation = model.GateLocation,
                    BaggageHandling = model.BaggageHandling,
                    EaseOfOnlineBooking = model.EaseOfOnlineBooking,
                    DepartureArrivalTimeConvenient = model.DepartureArrivalTimeConvenient,

                    DepartureDelayInMinutes = model.DepartureDelayInMinutes,
                    ArrivalDelayInMinutes = model.ArrivalDelayInMinutes,

                    Column1 = 0,
                    Id = 0,
                    Satisfaction = ""
                };

                // 2. Apelăm API-ul
                var prediction = await _satisfactionService.PredictSatisfactionAsync(input);

                if (string.IsNullOrEmpty(prediction))
                {
                    ModelState.AddModelError(string.Empty, "Eroare: API-ul nu a returnat nicio predicție.");
                }
                else
                {
                    // Setăm rezultatul în View
                    model.PredictedSatisfaction = prediction;

                    // 3. SALVĂM ÎN ISTORIC (BAZA DE DATE)
                    // Copiem TOATE câmpurile manual
                    var historyItem = new SatisfactionPredictionHistory
                    {
                        CreatedAt = DateTime.Now,
                        PredictedSatisfaction = prediction,

                        // Date Pasager
                        Gender = model.Gender,
                        CustomerType = model.CustomerType,
                        Age = model.Age,
                        TypeOfTravel = model.TypeOfTravel,
                        Class = model.Class,
                        FlightDistance = model.FlightDistance,

                        // Servicii
                        SeatComfort = model.SeatComfort,
                        InflightWifiService = model.InflightWifiService,
                        FoodAndDrink = model.FoodAndDrink,
                        InflightEntertainment = model.InflightEntertainment,
                        OnlineBoarding = model.OnlineBoarding,
                        LegRoomService = model.LegRoomService,
                        OnBoardService = model.OnBoardService,
                        CheckinService = model.CheckinService,
                        InflightService = model.InflightService,
                        Cleanliness = model.Cleanliness,
                        GateLocation = model.GateLocation,
                        BaggageHandling = model.BaggageHandling,
                        EaseOfOnlineBooking = model.EaseOfOnlineBooking,
                        DepartureArrivalTimeConvenient = model.DepartureArrivalTimeConvenient,

                        // Întârzieri
                        DepartureDelayInMinutes = model.DepartureDelayInMinutes,
                        ArrivalDelayInMinutes = model.ArrivalDelayInMinutes
                    };

                    _context.PredictionHistories.Add(historyItem);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Eroare tehnică: {ex.Message}");
            }

            return View(model);
        }

        // GET: /SatisfactionPrediction/History
        [HttpGet]
        public async Task<IActionResult> History()
        {
            // Returnăm lista completă din baza de date, ordonată descrescător după dată
            var history = await _context.PredictionHistories
                                        .OrderByDescending(p => p.CreatedAt)
                                        .ToListAsync();
            return View(history);
        }
    }
}