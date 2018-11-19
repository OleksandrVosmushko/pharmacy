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
    public class ReceiptsController : Controller
    {
        private readonly pharmacyContext _context;

        public ReceiptsController(pharmacyContext context)
        {
            _context = context;
        }

        // GET: Receipts
        public async Task<IActionResult> Index()
        {
            var pharmacyContext = _context.Receipt.Include(r => r.Medicine).Include(r => r.Pharmacist);
            return View(await pharmacyContext.ToListAsync());
        }

        // GET: Receipts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receipt = await _context.Receipt
                .Include(r => r.Medicine)
                .Include(r => r.Pharmacist)
                .FirstOrDefaultAsync(m => m.Receiptid == id);
            if (receipt == null)
            {
                return NotFound();
            }

            return View(receipt);
        }

        // GET: Receipts/Create
        public IActionResult Create()
        {
            ViewData["Medicineid"] = new SelectList(_context.Medicine, "Medicineid", "Medicineid");
            ViewData["Pharmacistid"] = new SelectList(_context.Pharmacist, "Pharmacistid", "Pharmacistid");
            return View();
        }

        // POST: Receipts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Receiptid,Medicineid,Pharmacistid,Quantity")] Receipt receipt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(receipt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Medicineid"] = new SelectList(_context.Medicine, "Medicineid", "Medicineid", receipt.Medicineid);
            ViewData["Pharmacistid"] = new SelectList(_context.Pharmacist, "Pharmacistid", "Pharmacistid", receipt.Pharmacistid);
            return View(receipt);
        }

        // GET: Receipts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receipt = await _context.Receipt.FindAsync(id);
            if (receipt == null)
            {
                return NotFound();
            }
            ViewData["Medicineid"] = new SelectList(_context.Medicine, "Medicineid", "Medicineid", receipt.Medicineid);
            ViewData["Pharmacistid"] = new SelectList(_context.Pharmacist, "Pharmacistid", "Pharmacistid", receipt.Pharmacistid);
            return View(receipt);
        }

        // POST: Receipts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Receiptid,Medicineid,Pharmacistid,Quantity")] Receipt receipt)
        {
            if (id != receipt.Receiptid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receipt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceiptExists(receipt.Receiptid))
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
            ViewData["Medicineid"] = new SelectList(_context.Medicine, "Medicineid", "Medicineid", receipt.Medicineid);
            ViewData["Pharmacistid"] = new SelectList(_context.Pharmacist, "Pharmacistid", "Pharmacistid", receipt.Pharmacistid);
            return View(receipt);
        }

        // GET: Receipts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receipt = await _context.Receipt
                .Include(r => r.Medicine)
                .Include(r => r.Pharmacist)
                .FirstOrDefaultAsync(m => m.Receiptid == id);
            if (receipt == null)
            {
                return NotFound();
            }

            return View(receipt);
        }

        // POST: Receipts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var receipt = await _context.Receipt.FindAsync(id);
            _context.Receipt.Remove(receipt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceiptExists(int id)
        {
            return _context.Receipt.Any(e => e.Receiptid == id);
        }
    }
}
