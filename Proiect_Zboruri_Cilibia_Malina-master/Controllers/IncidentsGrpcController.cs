using Grpc.Net.Client;
using GrpcIncidentsService; 
using Microsoft.AspNetCore.Mvc;

namespace Proiect_Zboruri_Cilibia_Malina.Controllers
{
    public class IncidentsGrpcController : Controller
    {
        private readonly GrpcChannel _channel;

        public IncidentsGrpcController()
        {
            _channel = GrpcChannel.ForAddress("https://localhost:7199");
        }

        [HttpGet]
        public IActionResult Index()
        {
            var client = new IncidentService.IncidentServiceClient(_channel);

            var incidentList = client.GetAll(new Empty());

            return View(incidentList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Incident incident)
        {
            try
            {
                var client = new IncidentService.IncidentServiceClient(_channel);

                client.Insert(incident);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(incident);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var client = new IncidentService.IncidentServiceClient(_channel);

            var incident = client.Get(new IncidentId { Id = id });

            if (incident == null)
            {
                return NotFound();
            }

            return View(incident);
        }

        [HttpPost]
        public IActionResult Edit(Incident incident)
        {
            try
            {
                var client = new IncidentService.IncidentServiceClient(_channel);

                client.Update(incident);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(incident);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var client = new IncidentService.IncidentServiceClient(_channel);

            var incident = client.Get(new IncidentId { Id = id });

            if (incident == null)
            {
                return NotFound();
            }

            return View(incident);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var client = new IncidentService.IncidentServiceClient(_channel);

            client.Delete(new IncidentId { Id = id });

            return RedirectToAction(nameof(Index));
        }
    }
}