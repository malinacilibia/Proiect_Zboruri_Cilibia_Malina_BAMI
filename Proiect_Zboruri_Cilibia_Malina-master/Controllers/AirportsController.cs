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
    public class AirportsController : Controller
    {
        private readonly FlightContext _context;

        public AirportsController(FlightContext context)
        {
            _context = context;
        }

        // GET: Airports
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["CitySortParm"] = String.IsNullOrEmpty(sortOrder) ? "city_desc" : "";
            ViewData["CodeSortParm"] = sortOrder == "Code" ? "code_desc" : "Code";

            ViewData["CurrentFilter"] = searchString;

            var airports = from a in _context.Airports
                           select a;

            if (!String.IsNullOrEmpty(searchString))
            {
                airports = airports.Where(s => s.City.Contains(searchString) || s.Code.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "city_desc":
                    airports = airports.OrderByDescending(s => s.City);
                    break;
                case "Code":
                    airports = airports.OrderBy(s => s.Code);
                    break;
                case "code_desc":
                    airports = airports.OrderByDescending(s => s.Code);
                    break;
                default:
                    airports = airports.OrderBy(s => s.City); 
                    break;
            }

            return View(await airports.AsNoTracking().ToListAsync());
        }

        // GET: Airports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airport = await _context.Airports
                .FirstOrDefaultAsync(m => m.ID == id);
            if (airport == null)
            {
                return NotFound();
            }

            return View(airport);
        }

        // GET: Airports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Airports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Code,City")] Airport airport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(airport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(airport);
        }

        // GET: Airports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airport = await _context.Airports.FindAsync(id);
            if (airport == null)
            {
                return NotFound();
            }
            return View(airport);
        }

        // POST: Airports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Code,City")] Airport airport)
        {
            if (id != airport.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(airport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AirportExists(airport.ID))
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
            return View(airport);
        }

        // GET: Airports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airport = await _context.Airports
                .FirstOrDefaultAsync(m => m.ID == id);
            if (airport == null)
            {
                return NotFound();
            }

            return View(airport);
        }

        // POST: Airports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var airport = await _context.Airports.FindAsync(id);
            if (airport != null)
            {
                _context.Airports.Remove(airport);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AirportExists(int id)
        {
            return _context.Airports.Any(e => e.ID == id);
        }
    }
}
