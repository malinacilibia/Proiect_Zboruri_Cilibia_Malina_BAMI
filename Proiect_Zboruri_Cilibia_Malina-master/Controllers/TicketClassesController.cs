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
    public class TicketClassesController : Controller
    {
        private readonly FlightContext _context;

        public TicketClassesController(FlightContext context)
        {
            _context = context;
        }

        // GET: TicketClasses
        public async Task<IActionResult> Index()
        {
            return View(await _context.TicketClasses.ToListAsync());
        }

        // GET: TicketClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketClass = await _context.TicketClasses
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ticketClass == null)
            {
                return NotFound();
            }

            return View(ticketClass);
        }

        // GET: TicketClasses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TicketClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] TicketClass ticketClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticketClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ticketClass);
        }

        // GET: TicketClasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketClass = await _context.TicketClasses.FindAsync(id);
            if (ticketClass == null)
            {
                return NotFound();
            }
            return View(ticketClass);
        }

        // POST: TicketClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] TicketClass ticketClass)
        {
            if (id != ticketClass.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketClassExists(ticketClass.ID))
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
            return View(ticketClass);
        }

        // GET: TicketClasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketClass = await _context.TicketClasses
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ticketClass == null)
            {
                return NotFound();
            }

            return View(ticketClass);
        }

        // POST: TicketClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticketClass = await _context.TicketClasses.FindAsync(id);
            if (ticketClass != null)
            {
                _context.TicketClasses.Remove(ticketClass);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketClassExists(int id)
        {
            return _context.TicketClasses.Any(e => e.ID == id);
        }
    }
}
