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
    public class PharmacistsController : Controller
    {
        private readonly pharmacyContext _context;

        public PharmacistsController(pharmacyContext context)
        {
            _context = context;
        }

        // GET: Pharmacists
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pharmacist.ToListAsync());
        }

        // GET: Pharmacists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pharmacist = await _context.Pharmacist
                .FirstOrDefaultAsync(m => m.Pharmacistid == id);
            if (pharmacist == null)
            {
                return NotFound();
            }

            return View(pharmacist);
        }

        // GET: Pharmacists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pharmacists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Pharmacistid,Firstname,Lastname,Salary,Birthday")] Pharmacist pharmacist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pharmacist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pharmacist);
        }

        // GET: Pharmacists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pharmacist = await _context.Pharmacist.FindAsync(id);
            if (pharmacist == null)
            {
                return NotFound();
            }
            return View(pharmacist);
        }

        // POST: Pharmacists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Pharmacistid,Firstname,Lastname,Salary,Birthday")] Pharmacist pharmacist)
        {
            if (id != pharmacist.Pharmacistid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pharmacist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PharmacistExists(pharmacist.Pharmacistid))
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
            return View(pharmacist);
        }

        // GET: Pharmacists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pharmacist = await _context.Pharmacist
                .FirstOrDefaultAsync(m => m.Pharmacistid == id);
            if (pharmacist == null)
            {
                return NotFound();
            }

            return View(pharmacist);
        }

        // POST: Pharmacists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pharmacist = await _context.Pharmacist.FindAsync(id);
            _context.Pharmacist.Remove(pharmacist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PharmacistExists(int id)
        {
            return _context.Pharmacist.Any(e => e.Pharmacistid == id);
        }
    }
}
