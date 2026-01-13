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
    public class BookingsController : Controller
    {
        private readonly FlightContext _context;

        public BookingsController(FlightContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";

            ViewData["CurrentFilter"] = searchString;

            var bookings = from b in _context.Bookings
                           .Include(b => b.Flight)
                           .Include(b => b.Passenger)
                           .Include(b => b.TicketClass)
                           select b;

            if (!String.IsNullOrEmpty(searchString))
            {
                bookings = bookings.Where(s => s.Passenger.LastName.Contains(searchString)
                                            || s.Flight.FlightNumber.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "date_desc":
                    bookings = bookings.OrderByDescending(s => s.BookingDate);
                    break;
                case "Price":
                    bookings = bookings.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    bookings = bookings.OrderByDescending(s => s.Price);
                    break;
                default:
                    bookings = bookings.OrderBy(s => s.BookingDate); 
                    break;
            }

            return View(await bookings.AsNoTracking().ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Flight)
                .Include(b => b.Passenger)
                .Include(b => b.TicketClass)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["FlightID"] = new SelectList(_context.Flights, "ID", "FlightNumber");
            ViewData["PassengerID"] = new SelectList(_context.Passengers, "ID", "FullName");
            ViewData["TicketClassID"] = new SelectList(_context.TicketClasses, "ID", "Name");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Price,BookingDate,FlightID,PassengerID,TicketClassID")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FlightID"] = new SelectList(_context.Flights, "ID", "FlightNumber", booking.FlightID);
            ViewData["PassengerID"] = new SelectList(_context.Passengers, "ID", "Email", booking.PassengerID);
            ViewData["TicketClassID"] = new SelectList(_context.TicketClasses, "ID", "Name", booking.TicketClassID);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["FlightID"] = new SelectList(_context.Flights, "ID", "FlightNumber", booking.FlightID);
            ViewData["PassengerID"] = new SelectList(_context.Passengers, "ID", "FullName", booking.PassengerID);
            ViewData["TicketClassID"] = new SelectList(_context.TicketClasses, "ID", "Name", booking.TicketClassID);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Price,BookingDate,FlightID,PassengerID,TicketClassID")] Booking booking)
        {
            if (id != booking.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.ID))
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
            ViewData["FlightID"] = new SelectList(_context.Flights, "ID", "FlightNumber", booking.FlightID);
            ViewData["PassengerID"] = new SelectList(_context.Passengers, "ID", "Email", booking.PassengerID);
            ViewData["TicketClassID"] = new SelectList(_context.TicketClasses, "ID", "Name", booking.TicketClassID);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Flight)
                .Include(b => b.Passenger)
                .Include(b => b.TicketClass)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.ID == id);
        }
    }
}
