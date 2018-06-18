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
    public class PrzystosowanieSalisController : Controller
    {
        private readonly TestowyZKartki01Context _context;

        public PrzystosowanieSalisController(TestowyZKartki01Context context)
        {
            _context = context;
        }

        // GET: PrzystosowanieSalis
        public async Task<IActionResult> Index()
        {
            var testowyZKartki01Context = _context.PrzystosowanieSaliSet.Include(p => p.Dyscyplina).Include(p => p.Poziom).Include(p => p.Sala);
            return View(await testowyZKartki01Context.ToListAsync());
        }

        // GET: PrzystosowanieSalis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przystosowanieSali = await _context.PrzystosowanieSaliSet
                .Include(p => p.Dyscyplina)
                .Include(p => p.Poziom)
                .Include(p => p.Sala)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (przystosowanieSali == null)
            {
                return NotFound();
            }

            return View(przystosowanieSali);
        }

        // GET: PrzystosowanieSalis/Create
        public IActionResult Create()
        {
            ViewData["DyscyplinaID"] = new SelectList(_context.DyscyplinaSet, "ID", "ID");
            ViewData["PoziomID"] = new SelectList(_context.PoziomZaawansowaniaSet, "ID", "ID");
            ViewData["SalaID"] = new SelectList(_context.SalaSet, "ID", "ID");
            return View();
        }

        // POST: PrzystosowanieSalis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PoziomID,DyscyplinaID,SalaID,Pojemnosc,StawkaZaZajecia")] PrzystosowanieSali przystosowanieSali)
        {
            if (ModelState.IsValid)
            {
                _context.Add(przystosowanieSali);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DyscyplinaID"] = new SelectList(_context.DyscyplinaSet, "ID", "ID", przystosowanieSali.DyscyplinaID);
            ViewData["PoziomID"] = new SelectList(_context.PoziomZaawansowaniaSet, "ID", "ID", przystosowanieSali.PoziomID);
            ViewData["SalaID"] = new SelectList(_context.SalaSet, "ID", "ID", przystosowanieSali.SalaID);
            return View(przystosowanieSali);
        }

        // GET: PrzystosowanieSalis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przystosowanieSali = await _context.PrzystosowanieSaliSet.SingleOrDefaultAsync(m => m.ID == id);
            if (przystosowanieSali == null)
            {
                return NotFound();
            }
            ViewData["DyscyplinaID"] = new SelectList(_context.DyscyplinaSet, "ID", "ID", przystosowanieSali.DyscyplinaID);
            ViewData["PoziomID"] = new SelectList(_context.PoziomZaawansowaniaSet, "ID", "ID", przystosowanieSali.PoziomID);
            ViewData["SalaID"] = new SelectList(_context.SalaSet, "ID", "ID", przystosowanieSali.SalaID);
            return View(przystosowanieSali);
        }

        // POST: PrzystosowanieSalis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PoziomID,DyscyplinaID,SalaID,Pojemnosc,StawkaZaZajecia")] PrzystosowanieSali przystosowanieSali)
        {
            if (id != przystosowanieSali.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(przystosowanieSali);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrzystosowanieSaliExists(przystosowanieSali.ID))
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
            ViewData["DyscyplinaID"] = new SelectList(_context.DyscyplinaSet, "ID", "ID", przystosowanieSali.DyscyplinaID);
            ViewData["PoziomID"] = new SelectList(_context.PoziomZaawansowaniaSet, "ID", "ID", przystosowanieSali.PoziomID);
            ViewData["SalaID"] = new SelectList(_context.SalaSet, "ID", "ID", przystosowanieSali.SalaID);
            return View(przystosowanieSali);
        }

        // GET: PrzystosowanieSalis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przystosowanieSali = await _context.PrzystosowanieSaliSet
                .Include(p => p.Dyscyplina)
                .Include(p => p.Poziom)
                .Include(p => p.Sala)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (przystosowanieSali == null)
            {
                return NotFound();
            }

            return View(przystosowanieSali);
        }

        // POST: PrzystosowanieSalis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var przystosowanieSali = await _context.PrzystosowanieSaliSet.SingleOrDefaultAsync(m => m.ID == id);
            _context.PrzystosowanieSaliSet.Remove(przystosowanieSali);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrzystosowanieSaliExists(int id)
        {
            return _context.PrzystosowanieSaliSet.Any(e => e.ID == id);
        }
    }
}
