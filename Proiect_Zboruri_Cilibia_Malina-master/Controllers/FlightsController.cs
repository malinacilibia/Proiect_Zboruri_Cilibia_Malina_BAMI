using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect_Zboruri_Cilibia_Malina.Data;
using Proiect_Zboruri_Cilibia_Malina.Models;

namespace Proiect_Zboruri_Cilibia_Malina.Controllers
{
    public class FlightsController : Controller
    {
        private readonly FlightContext _context;

        public FlightsController(FlightContext context)
        {
            _context = context;
        }

        // GET: Flights
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["DurationSortParm"] = sortOrder == "Duration" ? "duration_desc" : "Duration";
            ViewData["CurrentFilter"] = searchString;

            var flights = from f in _context.Flights
                              .Include(f => f.Airline)
                              .Include(f => f.Airport)
                          select f;

            if (!String.IsNullOrEmpty(searchString))
            {
                flights = flights.Where(s => s.Airport.Code.Contains(searchString)
                              || s.Airline.Name.Contains(searchString)
                              || s.Destination.Contains(searchString)   
                              || s.FlightNumber.Contains(searchString)); 
            }

            switch (sortOrder)
            {
                case "Date":
                    flights = flights.OrderBy(s => s.DepartureTime);
                    break;
                case "date_desc":
                    flights = flights.OrderByDescending(s => s.DepartureTime);
                    break;
                case "Duration":
                    flights = flights.OrderBy(s => s.Duration);
                    break;
                case "duration_desc":
                    flights = flights.OrderByDescending(s => s.Duration);
                    break;
                default:
                    flights = flights.OrderBy(s => s.DepartureTime); 
                    break;
            }

            return View(await flights.AsNoTracking().ToListAsync());

        }

        // GET: Flights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .Include(f => f.Airline)
                .Include(f => f.Airport)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // GET: Flights/Create
        public IActionResult Create()
        {
            ViewData["AirlineID"] = new SelectList(_context.Airlines, "ID", "Name");
            ViewData["AirportID"] = new SelectList(_context.Airports, "ID", "Code");
            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FlightNumber,Destination,DepartureTime,Duration,AirlineID,AirportID")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AirlineID"] = new SelectList(_context.Airlines, "ID", "Name", flight.AirlineID);
            ViewData["AirportID"] = new SelectList(_context.Airports, "ID", "Code", flight.AirportID);
            return View(flight);
        }

        // GET: Flights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }
            ViewData["AirlineID"] = new SelectList(_context.Airlines, "ID", "Name", flight.AirlineID);
            ViewData["AirportID"] = new SelectList(_context.Airports, "ID", "Code", flight.AirportID);
            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FlightNumber,Destination,DepartureTime,Duration,AirlineID,AirportID")] Flight flight)
        {
            if (id != flight.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExists(flight.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AirlineID"] = new SelectList(_context.Airlines, "ID", "Name", flight.AirlineID);
            ViewData["AirportID"] = new SelectList(_context.Airports, "ID", "Code", flight.AirportID);
            return View(flight);
        }

        // GET: Flights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .Include(f => f.Airline)
                .Include(f => f.Airport)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight != null)
            {
                _context.Flights.Remove(flight);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightExists(int id)
        {
            return _context.Flights.Any(e => e.ID == id);
        }
    }
}
