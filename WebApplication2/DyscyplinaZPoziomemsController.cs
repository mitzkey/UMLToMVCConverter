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
    public class DyscyplinaZPoziomemsController : Controller
    {
        private readonly TestowyZKartki01Context _context;

        public DyscyplinaZPoziomemsController(TestowyZKartki01Context context)
        {
            _context = context;
        }

        // GET: DyscyplinaZPoziomems
        public async Task<IActionResult> Index()
        {
            var testowyZKartki01Context = _context.DyscyplinaZPoziomemSet.Include(d => d.Dyscyplina).Include(d => d.PoziomZaawansowania);
            return View(await testowyZKartki01Context.ToListAsync());
        }

        // GET: DyscyplinaZPoziomems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dyscyplinaZPoziomem = await _context.DyscyplinaZPoziomemSet
                .Include(d => d.Dyscyplina)
                .Include(d => d.PoziomZaawansowania)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (dyscyplinaZPoziomem == null)
            {
                return NotFound();
            }

            return View(dyscyplinaZPoziomem);
        }

        // GET: DyscyplinaZPoziomems/Create
        public IActionResult Create()
        {
            ViewData["DyscyplinaID"] = new SelectList(_context.DyscyplinaSet, "ID", "ID");
            ViewData["PoziomZaawansowaniaID"] = new SelectList(_context.PoziomZaawansowaniaSet, "ID", "ID");
            return View();
        }

        // POST: DyscyplinaZPoziomems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DyscyplinaID,PoziomZaawansowaniaID,Nazwa")] DyscyplinaZPoziomem dyscyplinaZPoziomem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dyscyplinaZPoziomem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DyscyplinaID"] = new SelectList(_context.DyscyplinaSet, "ID", "ID", dyscyplinaZPoziomem.DyscyplinaID);
            ViewData["PoziomZaawansowaniaID"] = new SelectList(_context.PoziomZaawansowaniaSet, "ID", "ID", dyscyplinaZPoziomem.PoziomZaawansowaniaID);
            return View(dyscyplinaZPoziomem);
        }

        // GET: DyscyplinaZPoziomems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dyscyplinaZPoziomem = await _context.DyscyplinaZPoziomemSet.SingleOrDefaultAsync(m => m.ID == id);
            if (dyscyplinaZPoziomem == null)
            {
                return NotFound();
            }
            ViewData["DyscyplinaID"] = new SelectList(_context.DyscyplinaSet, "ID", "ID", dyscyplinaZPoziomem.DyscyplinaID);
            ViewData["PoziomZaawansowaniaID"] = new SelectList(_context.PoziomZaawansowaniaSet, "ID", "ID", dyscyplinaZPoziomem.PoziomZaawansowaniaID);
            return View(dyscyplinaZPoziomem);
        }

        // POST: DyscyplinaZPoziomems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DyscyplinaID,PoziomZaawansowaniaID,Nazwa")] DyscyplinaZPoziomem dyscyplinaZPoziomem)
        {
            if (id != dyscyplinaZPoziomem.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dyscyplinaZPoziomem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DyscyplinaZPoziomemExists(dyscyplinaZPoziomem.ID))
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
            ViewData["DyscyplinaID"] = new SelectList(_context.DyscyplinaSet, "ID", "ID", dyscyplinaZPoziomem.DyscyplinaID);
            ViewData["PoziomZaawansowaniaID"] = new SelectList(_context.PoziomZaawansowaniaSet, "ID", "ID", dyscyplinaZPoziomem.PoziomZaawansowaniaID);
            return View(dyscyplinaZPoziomem);
        }

        // GET: DyscyplinaZPoziomems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dyscyplinaZPoziomem = await _context.DyscyplinaZPoziomemSet
                .Include(d => d.Dyscyplina)
                .Include(d => d.PoziomZaawansowania)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (dyscyplinaZPoziomem == null)
            {
                return NotFound();
            }

            return View(dyscyplinaZPoziomem);
        }

        // POST: DyscyplinaZPoziomems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dyscyplinaZPoziomem = await _context.DyscyplinaZPoziomemSet.SingleOrDefaultAsync(m => m.ID == id);
            _context.DyscyplinaZPoziomemSet.Remove(dyscyplinaZPoziomem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DyscyplinaZPoziomemExists(int id)
        {
            return _context.DyscyplinaZPoziomemSet.Any(e => e.ID == id);
        }
    }
}
