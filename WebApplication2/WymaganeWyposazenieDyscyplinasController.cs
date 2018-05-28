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
    public class WymaganeWyposazenieDyscyplinasController : Controller
    {
        private readonly TestowyZKartki01Context _context;

        public WymaganeWyposazenieDyscyplinasController(TestowyZKartki01Context context)
        {
            _context = context;
        }

        // GET: WymaganeWyposazenieDyscyplinas
        public async Task<IActionResult> Index()
        {
            var testowyZKartki01Context = _context.WymaganeWyposazenieDyscyplina.Include(w => w.WymaganeWyposazenie);
            return View(await testowyZKartki01Context.ToListAsync());
        }

        // GET: WymaganeWyposazenieDyscyplinas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wymaganeWyposazenieDyscyplina = await _context.WymaganeWyposazenieDyscyplina
                .Include(w => w.WymaganeWyposazenie)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (wymaganeWyposazenieDyscyplina == null)
            {
                return NotFound();
            }

            return View(wymaganeWyposazenieDyscyplina);
        }

        // GET: WymaganeWyposazenieDyscyplinas/Create
        public IActionResult Create()
        {
            ViewData["WymaganeWyposazenieID"] = new SelectList(_context.Wyposażenie, "ID", "ID");
            return View();
        }

        // POST: WymaganeWyposazenieDyscyplinas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,WymaganeWyposazenieID")] WymaganeWyposazenieDyscyplina wymaganeWyposazenieDyscyplina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wymaganeWyposazenieDyscyplina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WymaganeWyposazenieID"] = new SelectList(_context.Wyposażenie, "ID", "ID", wymaganeWyposazenieDyscyplina.WymaganeWyposazenieID);
            return View(wymaganeWyposazenieDyscyplina);
        }

        // GET: WymaganeWyposazenieDyscyplinas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wymaganeWyposazenieDyscyplina = await _context.WymaganeWyposazenieDyscyplina.SingleOrDefaultAsync(m => m.ID == id);
            if (wymaganeWyposazenieDyscyplina == null)
            {
                return NotFound();
            }
            ViewData["WymaganeWyposazenieID"] = new SelectList(_context.Wyposażenie, "ID", "ID", wymaganeWyposazenieDyscyplina.WymaganeWyposazenieID);
            return View(wymaganeWyposazenieDyscyplina);
        }

        // POST: WymaganeWyposazenieDyscyplinas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,WymaganeWyposazenieID")] WymaganeWyposazenieDyscyplina wymaganeWyposazenieDyscyplina)
        {
            if (id != wymaganeWyposazenieDyscyplina.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wymaganeWyposazenieDyscyplina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WymaganeWyposazenieDyscyplinaExists(wymaganeWyposazenieDyscyplina.ID))
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
            ViewData["WymaganeWyposazenieID"] = new SelectList(_context.Wyposażenie, "ID", "ID", wymaganeWyposazenieDyscyplina.WymaganeWyposazenieID);
            return View(wymaganeWyposazenieDyscyplina);
        }

        // GET: WymaganeWyposazenieDyscyplinas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wymaganeWyposazenieDyscyplina = await _context.WymaganeWyposazenieDyscyplina
                .Include(w => w.WymaganeWyposazenie)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (wymaganeWyposazenieDyscyplina == null)
            {
                return NotFound();
            }

            return View(wymaganeWyposazenieDyscyplina);
        }

        // POST: WymaganeWyposazenieDyscyplinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wymaganeWyposazenieDyscyplina = await _context.WymaganeWyposazenieDyscyplina.SingleOrDefaultAsync(m => m.ID == id);
            _context.WymaganeWyposazenieDyscyplina.Remove(wymaganeWyposazenieDyscyplina);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WymaganeWyposazenieDyscyplinaExists(int id)
        {
            return _context.WymaganeWyposazenieDyscyplina.Any(e => e.ID == id);
        }
    }
}
