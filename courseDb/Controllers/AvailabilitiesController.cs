using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using courseDb.Models;

namespace courseDb.Controllers
{
    public class AvailabilitiesController : Controller
    {
        private readonly pharmacyContext _context;

        public AvailabilitiesController(pharmacyContext context)
        {
            _context = context;
        }

        // GET: Availabilities
        public async Task<IActionResult> Index()
        {
            var pharmacyContext = _context.Availability.Include(a => a.Medicine);
            return View(await pharmacyContext.ToListAsync());
        }

        // GET: Availabilities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availability = await _context.Availability
                .Include(a => a.Medicine)
                .FirstOrDefaultAsync(m => m.Availabilityid == id);
            if (availability == null)
            {
                return NotFound();
            }

            return View(availability);
        }

        // GET: Availabilities/Create
        public IActionResult Create()
        {
            ViewData["Medicineid"] = new SelectList(_context.Medicine, "Medicineid", "Medicineid");
            return View();
        }

        // POST: Availabilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Availabilityid,Medicineid,Quantity,Price,Productiontime")] Availability availability)
        {
            if (ModelState.IsValid)
            {
                _context.Add(availability);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Medicineid"] = new SelectList(_context.Medicine, "Medicineid", "Medicineid", availability.Medicineid);
            return View(availability);
        }

        // GET: Availabilities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availability = await _context.Availability.FindAsync(id);
            if (availability == null)
            {
                return NotFound();
            }
            ViewData["Medicineid"] = new SelectList(_context.Medicine, "Medicineid", "Medicineid", availability.Medicineid);
            return View(availability);
        }

        // POST: Availabilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Availabilityid,Medicineid,Quantity,Price,Productiontime")] Availability availability)
        {
            if (id != availability.Availabilityid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(availability);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvailabilityExists(availability.Availabilityid))
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
            ViewData["Medicineid"] = new SelectList(_context.Medicine, "Medicineid", "Medicineid", availability.Medicineid);
            return View(availability);
        }

        // GET: Availabilities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var availability = await _context.Availability
                .Include(a => a.Medicine)
                .FirstOrDefaultAsync(m => m.Availabilityid == id);
            if (availability == null)
            {
                return NotFound();
            }

            return View(availability);
        }

        // POST: Availabilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var availability = await _context.Availability.FindAsync(id);
            _context.Availability.Remove(availability);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvailabilityExists(int id)
        {
            return _context.Availability.Any(e => e.Availabilityid == id);
        }
    }
}
