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
    public class TerminsController : Controller
    {
        private readonly TestowyZKartki01Context _context;

        public TerminsController(TestowyZKartki01Context context)
        {
            _context = context;
        }

        // GET: Termins
        public async Task<IActionResult> Index()
        {
            var testowyZKartki01Context = _context.TerminSet.Include(t => t.Grafik);
            return View(await testowyZKartki01Context.ToListAsync());
        }

        // GET: Termins/Details/5
        public async Task<IActionResult> Details(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var termin = await _context.TerminSet
                .Include(t => t.Grafik)
                .SingleOrDefaultAsync(m => m.Dzien == id);
            if (termin == null)
            {
                return NotFound();
            }

            return View(termin);
        }

        // GET: Termins/Create
        public IActionResult Create()
        {
            ViewData["GrafikID"] = new SelectList(_context.GrafikSet, "ID", "ID");
            return View();
        }

        // POST: Termins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GrafikID,Dzien,GodzinaRozpoczecia")] Termin termin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(termin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GrafikID"] = new SelectList(_context.GrafikSet, "ID", "ID", termin.GrafikID);
            return View(termin);
        }

        // GET: Termins/Edit/5
        public async Task<IActionResult> Edit(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var termin = await _context.TerminSet.SingleOrDefaultAsync(m => m.Dzien == id);
            if (termin == null)
            {
                return NotFound();
            }
            ViewData["GrafikID"] = new SelectList(_context.GrafikSet, "ID", "ID", termin.GrafikID);
            return View(termin);
        }

        // POST: Termins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DateTime? id, [Bind("GrafikID,Dzien,GodzinaRozpoczecia")] Termin termin)
        {
            if (id != termin.Dzien)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(termin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TerminExists(termin.Dzien))
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
            ViewData["GrafikID"] = new SelectList(_context.GrafikSet, "ID", "ID", termin.GrafikID);
            return View(termin);
        }

        // GET: Termins/Delete/5
        public async Task<IActionResult> Delete(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var termin = await _context.TerminSet
                .Include(t => t.Grafik)
                .SingleOrDefaultAsync(m => m.Dzien == id);
            if (termin == null)
            {
                return NotFound();
            }

            return View(termin);
        }

        // POST: Termins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DateTime? id)
        {
            var termin = await _context.TerminSet.SingleOrDefaultAsync(m => m.Dzien == id);
            _context.TerminSet.Remove(termin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TerminExists(DateTime? id)
        {
            return _context.TerminSet.Any(e => e.Dzien == id);
        }
    }
}
