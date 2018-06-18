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
    public class BrakujaceWyposazeniePrzystosowanieSalisController : Controller
    {
        private readonly TestowyZKartki01Context _context;

        public BrakujaceWyposazeniePrzystosowanieSalisController(TestowyZKartki01Context context)
        {
            _context = context;
        }

        // GET: BrakujaceWyposazeniePrzystosowanieSalis
        public async Task<IActionResult> Index()
        {
            var testowyZKartki01Context = _context.BrakujaceWyposazeniePrzystosowanieSaliSet.Include(b => b.BrakujaceWyposazenie);
            return View(await testowyZKartki01Context.ToListAsync());
        }

        // GET: BrakujaceWyposazeniePrzystosowanieSalis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brakujaceWyposazeniePrzystosowanieSali = await _context.BrakujaceWyposazeniePrzystosowanieSaliSet
                .Include(b => b.BrakujaceWyposazenie)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (brakujaceWyposazeniePrzystosowanieSali == null)
            {
                return NotFound();
            }

            return View(brakujaceWyposazeniePrzystosowanieSali);
        }

        // GET: BrakujaceWyposazeniePrzystosowanieSalis/Create
        public IActionResult Create()
        {
            ViewData["BrakujaceWyposazenieID"] = new SelectList(_context.WyposażenieSet, "ID", "ID");
            return View();
        }

        // POST: BrakujaceWyposazeniePrzystosowanieSalis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,BrakujaceWyposazenieID")] BrakujaceWyposazeniePrzystosowanieSali brakujaceWyposazeniePrzystosowanieSali)
        {
            if (ModelState.IsValid)
            {
                _context.Add(brakujaceWyposazeniePrzystosowanieSali);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrakujaceWyposazenieID"] = new SelectList(_context.WyposażenieSet, "ID", "ID", brakujaceWyposazeniePrzystosowanieSali.BrakujaceWyposazenieID);
            return View(brakujaceWyposazeniePrzystosowanieSali);
        }

        // GET: BrakujaceWyposazeniePrzystosowanieSalis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brakujaceWyposazeniePrzystosowanieSali = await _context.BrakujaceWyposazeniePrzystosowanieSaliSet.SingleOrDefaultAsync(m => m.ID == id);
            if (brakujaceWyposazeniePrzystosowanieSali == null)
            {
                return NotFound();
            }
            ViewData["BrakujaceWyposazenieID"] = new SelectList(_context.WyposażenieSet, "ID", "ID", brakujaceWyposazeniePrzystosowanieSali.BrakujaceWyposazenieID);
            return View(brakujaceWyposazeniePrzystosowanieSali);
        }

        // POST: BrakujaceWyposazeniePrzystosowanieSalis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,BrakujaceWyposazenieID")] BrakujaceWyposazeniePrzystosowanieSali brakujaceWyposazeniePrzystosowanieSali)
        {
            if (id != brakujaceWyposazeniePrzystosowanieSali.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brakujaceWyposazeniePrzystosowanieSali);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrakujaceWyposazeniePrzystosowanieSaliExists(brakujaceWyposazeniePrzystosowanieSali.ID))
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
            ViewData["BrakujaceWyposazenieID"] = new SelectList(_context.WyposażenieSet, "ID", "ID", brakujaceWyposazeniePrzystosowanieSali.BrakujaceWyposazenieID);
            return View(brakujaceWyposazeniePrzystosowanieSali);
        }

        // GET: BrakujaceWyposazeniePrzystosowanieSalis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brakujaceWyposazeniePrzystosowanieSali = await _context.BrakujaceWyposazeniePrzystosowanieSaliSet
                .Include(b => b.BrakujaceWyposazenie)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (brakujaceWyposazeniePrzystosowanieSali == null)
            {
                return NotFound();
            }

            return View(brakujaceWyposazeniePrzystosowanieSali);
        }

        // POST: BrakujaceWyposazeniePrzystosowanieSalis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brakujaceWyposazeniePrzystosowanieSali = await _context.BrakujaceWyposazeniePrzystosowanieSaliSet.SingleOrDefaultAsync(m => m.ID == id);
            _context.BrakujaceWyposazeniePrzystosowanieSaliSet.Remove(brakujaceWyposazeniePrzystosowanieSali);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrakujaceWyposazeniePrzystosowanieSaliExists(int id)
        {
            return _context.BrakujaceWyposazeniePrzystosowanieSaliSet.Any(e => e.ID == id);
        }
    }
}
