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
    public class SzczegolyKwalifikacjisController : Controller
    {
        private readonly TestowyZKartki01Context _context;

        public SzczegolyKwalifikacjisController(TestowyZKartki01Context context)
        {
            _context = context;
        }

        // GET: SzczegolyKwalifikacjis
        public async Task<IActionResult> Index()
        {
            var testowyZKartki01Context = _context.SzczegolyKwalifikacji.Include(s => s.DyscyplinaZPoziomem).Include(s => s.Instruktor);
            return View(await testowyZKartki01Context.ToListAsync());
        }

        // GET: SzczegolyKwalifikacjis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var szczegolyKwalifikacji = await _context.SzczegolyKwalifikacji
                .Include(s => s.DyscyplinaZPoziomem)
                .Include(s => s.Instruktor)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (szczegolyKwalifikacji == null)
            {
                return NotFound();
            }

            return View(szczegolyKwalifikacji);
        }

        // GET: SzczegolyKwalifikacjis/Create
        public IActionResult Create()
        {
            ViewData["DyscyplinaZPoziomemID"] = new SelectList(_context.DyscyplinaZPoziomem, "ID", "ID");
            ViewData["InstruktorID"] = new SelectList(_context.Instruktor, "ID", "ID");
            return View();
        }

        // POST: SzczegolyKwalifikacjis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,InstruktorID,DyscyplinaZPoziomemID,Priorytet,Certyfikat,StawkaZaZajecia")] SzczegolyKwalifikacji szczegolyKwalifikacji)
        {
            if (ModelState.IsValid)
            {
                _context.Add(szczegolyKwalifikacji);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DyscyplinaZPoziomemID"] = new SelectList(_context.DyscyplinaZPoziomem, "ID", "ID", szczegolyKwalifikacji.DyscyplinaZPoziomemID);
            ViewData["InstruktorID"] = new SelectList(_context.Instruktor, "ID", "ID", szczegolyKwalifikacji.InstruktorID);
            return View(szczegolyKwalifikacji);
        }

        // GET: SzczegolyKwalifikacjis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var szczegolyKwalifikacji = await _context.SzczegolyKwalifikacji.SingleOrDefaultAsync(m => m.ID == id);
            if (szczegolyKwalifikacji == null)
            {
                return NotFound();
            }
            ViewData["DyscyplinaZPoziomemID"] = new SelectList(_context.DyscyplinaZPoziomem, "ID", "ID", szczegolyKwalifikacji.DyscyplinaZPoziomemID);
            ViewData["InstruktorID"] = new SelectList(_context.Instruktor, "ID", "ID", szczegolyKwalifikacji.InstruktorID);
            return View(szczegolyKwalifikacji);
        }

        // POST: SzczegolyKwalifikacjis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,InstruktorID,DyscyplinaZPoziomemID,Priorytet,Certyfikat,StawkaZaZajecia")] SzczegolyKwalifikacji szczegolyKwalifikacji)
        {
            if (id != szczegolyKwalifikacji.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(szczegolyKwalifikacji);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SzczegolyKwalifikacjiExists(szczegolyKwalifikacji.ID))
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
            ViewData["DyscyplinaZPoziomemID"] = new SelectList(_context.DyscyplinaZPoziomem, "ID", "ID", szczegolyKwalifikacji.DyscyplinaZPoziomemID);
            ViewData["InstruktorID"] = new SelectList(_context.Instruktor, "ID", "ID", szczegolyKwalifikacji.InstruktorID);
            return View(szczegolyKwalifikacji);
        }

        // GET: SzczegolyKwalifikacjis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var szczegolyKwalifikacji = await _context.SzczegolyKwalifikacji
                .Include(s => s.DyscyplinaZPoziomem)
                .Include(s => s.Instruktor)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (szczegolyKwalifikacji == null)
            {
                return NotFound();
            }

            return View(szczegolyKwalifikacji);
        }

        // POST: SzczegolyKwalifikacjis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var szczegolyKwalifikacji = await _context.SzczegolyKwalifikacji.SingleOrDefaultAsync(m => m.ID == id);
            _context.SzczegolyKwalifikacji.Remove(szczegolyKwalifikacji);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SzczegolyKwalifikacjiExists(int id)
        {
            return _context.SzczegolyKwalifikacji.Any(e => e.ID == id);
        }
    }
}
