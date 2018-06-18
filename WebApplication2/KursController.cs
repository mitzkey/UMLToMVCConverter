using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2
{
    public class KursController : Controller
    {
        private readonly TestowyZKartki01Context _context;

        public KursController(TestowyZKartki01Context context)
        {
            _context = context;
        }

        // GET: Kurs
        public async Task<IActionResult> Index()
        {
            var testowyZKartki01Context = _context.KursSet.Include(k => k.Grafik);
            return View(await testowyZKartki01Context.ToListAsync());
        }

        // GET: Kurs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kurs = await _context.KursSet
                .Include(k => k.Grafik)
                .SingleOrDefaultAsync(m => m.Kod == id);
            if (kurs == null)
            {
                return NotFound();
            }

            return View(kurs);
        }

        // GET: Kurs/Create
        public IActionResult Create()
        {
            ViewData["GrafikID"] = new SelectList(_context.GrafikSet, "ID", "ID");
            return View();
        }

        // POST: Kurs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Kod,GrafikID")] Kurs kurs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kurs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GrafikID"] = new SelectList(_context.GrafikSet, "ID", "ID", kurs.GrafikID);
            return View(kurs);
        }

        // GET: Kurs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kurs = await _context.KursSet.SingleOrDefaultAsync(m => m.Kod == id);
            if (kurs == null)
            {
                return NotFound();
            }
            ViewData["GrafikID"] = new SelectList(_context.GrafikSet, "ID", "ID", kurs.GrafikID);
            return View(kurs);
        }

        // POST: Kurs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Kod,GrafikID")] Kurs kurs)
        {
            if (id != kurs.Kod)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kurs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KursExists(kurs.Kod))
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
            ViewData["GrafikID"] = new SelectList(_context.GrafikSet, "ID", "ID", kurs.GrafikID);
            return View(kurs);
        }

        // GET: Kurs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kurs = await _context.KursSet
                .Include(k => k.Grafik)
                .SingleOrDefaultAsync(m => m.Kod == id);
            if (kurs == null)
            {
                return NotFound();
            }

            return View(kurs);
        }

        // POST: Kurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var kurs = await _context.KursSet.SingleOrDefaultAsync(m => m.Kod == id);
            _context.KursSet.Remove(kurs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KursExists(string id)
        {
            return _context.KursSet.Any(e => e.Kod == id);
        }
    }
}
