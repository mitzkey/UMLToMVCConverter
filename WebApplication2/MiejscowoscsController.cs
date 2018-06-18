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
    public class MiejscowoscsController : Controller
    {
        private readonly TestowyZKartki01Context _context;

        public MiejscowoscsController(TestowyZKartki01Context context)
        {
            _context = context;
        }

        // GET: Miejscowoscs
        public async Task<IActionResult> Index()
        {
            var testowyZKartki01Context = _context.MiejscowoscSet.Include(m => m.WojewodztwoMiejscowosci);
            return View(await testowyZKartki01Context.ToListAsync());
        }

        // GET: Miejscowoscs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var miejscowosc = await _context.MiejscowoscSet
                .Include(m => m.WojewodztwoMiejscowosci)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (miejscowosc == null)
            {
                return NotFound();
            }

            return View(miejscowosc);
        }

        // GET: Miejscowoscs/Create
        public IActionResult Create()
        {
            ViewData["WojewodztwoMiejscowosciID"] = new SelectList(_context.WojewodztwoSet, "ID", "ID");
            return View();
        }

        // POST: Miejscowoscs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,WojewodztwoMiejscowosciID,Nazwa,Aktualna")] Miejscowosc miejscowosc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(miejscowosc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WojewodztwoMiejscowosciID"] = new SelectList(_context.WojewodztwoSet, "ID", "ID", miejscowosc.WojewodztwoMiejscowosciID);
            return View(miejscowosc);
        }

        // GET: Miejscowoscs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var miejscowosc = await _context.MiejscowoscSet.SingleOrDefaultAsync(m => m.ID == id);
            if (miejscowosc == null)
            {
                return NotFound();
            }
            ViewData["WojewodztwoMiejscowosciID"] = new SelectList(_context.WojewodztwoSet, "ID", "ID", miejscowosc.WojewodztwoMiejscowosciID);
            return View(miejscowosc);
        }

        // POST: Miejscowoscs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,WojewodztwoMiejscowosciID,Nazwa,Aktualna")] Miejscowosc miejscowosc)
        {
            if (id != miejscowosc.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(miejscowosc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MiejscowoscExists(miejscowosc.ID))
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
            ViewData["WojewodztwoMiejscowosciID"] = new SelectList(_context.WojewodztwoSet, "ID", "ID", miejscowosc.WojewodztwoMiejscowosciID);
            return View(miejscowosc);
        }

        // GET: Miejscowoscs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var miejscowosc = await _context.MiejscowoscSet
                .Include(m => m.WojewodztwoMiejscowosci)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (miejscowosc == null)
            {
                return NotFound();
            }

            return View(miejscowosc);
        }

        // POST: Miejscowoscs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var miejscowosc = await _context.MiejscowoscSet.SingleOrDefaultAsync(m => m.ID == id);
            _context.MiejscowoscSet.Remove(miejscowosc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MiejscowoscExists(int id)
        {
            return _context.MiejscowoscSet.Any(e => e.ID == id);
        }
    }
}
