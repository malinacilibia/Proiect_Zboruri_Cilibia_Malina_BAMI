using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proiect_Zboruri_Cilibia_Malina.Data;
using Proiect_Zboruri_Cilibia_Malina.Models;
using Proiect_Zboruri_Cilibia_Malina.Models.DashboardStats;
using Proiect_Zboruri_Cilibia_Malina.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect_Zboruri_Cilibia_Malina.Controllers
{
    public class SatisfactionPredictionController : Controller
    {
        private readonly ISatisfactionPredictionService _satisfactionService;
        private readonly FlightContext _context; 

        public SatisfactionPredictionController(ISatisfactionPredictionService satisfactionService, FlightContext context)
        {
            _satisfactionService = satisfactionService;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new SatisfactionPredictionViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(SatisfactionPredictionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
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

                var prediction = await _satisfactionService.PredictSatisfactionAsync(input);

                if (string.IsNullOrEmpty(prediction))
                {
                    ModelState.AddModelError(string.Empty, "Eroare: API-ul nu a returnat nicio predicție.");
                }
                else
                {
                    model.PredictedSatisfaction = prediction;

                    var historyItem = new SatisfactionPredictionHistory
                    {
                        CreatedAt = DateTime.Now,
                        PredictedSatisfaction = prediction,

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

        [HttpGet]
        public async Task<IActionResult> History()
        {
            var history = await _context.PredictionHistories
                                        .OrderByDescending(p => p.CreatedAt)
                                        .ToListAsync();
            return View(history);
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard(DateTime? fromDate, DateTime? toDate)
        {
            var query = _context.PredictionHistories.AsQueryable();

            if (fromDate.HasValue)
            {
                query = query.Where(p => p.CreatedAt.Date >= fromDate.Value.Date);
            }
            if (toDate.HasValue)
            {
                query = query.Where(p => p.CreatedAt.Date <= toDate.Value.Date);
            }

            var totalPredictions = await query.CountAsync();

            var classStats = await query
                .GroupBy(p => p.Class)
                .Select(g => new ClassStat
                {
                    ClassName = g.Key,
                    AverageDistance = g.Average(x => x.FlightDistance),
                    Count = g.Count()
                })
                .ToListAsync();

            var allAges = await query.Select(p => p.Age).ToListAsync();
            var ageBuckets = new List<AgeBucketStat>
    {
        new AgeBucketStat { Label = "0-20", Count = 0 },
        new AgeBucketStat { Label = "20-40", Count = 0 },
        new AgeBucketStat { Label = "40-60", Count = 0 },
        new AgeBucketStat { Label = "> 60", Count = 0 }
    };

            foreach (var age in allAges)
            {
                if (age <= 20) ageBuckets[0].Count++;
                else if (age <= 40) ageBuckets[1].Count++;
                else if (age <= 60) ageBuckets[2].Count++;
                else ageBuckets[3].Count++;
            }

            var viewModel = new DashboardViewModel
            {
                TotalPredictions = totalPredictions,
                ClassStats = classStats,
                AgeBuckets = ageBuckets,
                FromDate = fromDate,
                ToDate = toDate
            };

            return View(viewModel);
        }
    }
}