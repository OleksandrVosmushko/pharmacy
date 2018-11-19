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
    public class MakersController : Controller
    {
        private readonly pharmacyContext _context;

        public MakersController(pharmacyContext context)
        {
            _context = context;
        }

        // GET: Makers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Maker.ToListAsync());
        }

        // GET: Makers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maker = await _context.Maker
                .FirstOrDefaultAsync(m => m.Makerid == id);
            if (maker == null)
            {
                return NotFound();
            }

            return View(maker);
        }

        // GET: Makers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Makers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Makerid,Makername,Country,Website")] Maker maker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(maker);
        }

        // GET: Makers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maker = await _context.Maker.FindAsync(id);
            if (maker == null)
            {
                return NotFound();
            }
            return View(maker);
        }

        // POST: Makers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Makerid,Makername,Country,Website")] Maker maker)
        {
            if (id != maker.Makerid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MakerExists(maker.Makerid))
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
            return View(maker);
        }

        // GET: Makers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maker = await _context.Maker
                .FirstOrDefaultAsync(m => m.Makerid == id);
            if (maker == null)
            {
                return NotFound();
            }

            return View(maker);
        }

        // POST: Makers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maker = await _context.Maker.FindAsync(id);
            _context.Maker.Remove(maker);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MakerExists(int id)
        {
            return _context.Maker.Any(e => e.Makerid == id);
        }
    }
}
