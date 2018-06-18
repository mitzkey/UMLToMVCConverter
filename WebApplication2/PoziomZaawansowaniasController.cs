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
    public class PoziomZaawansowaniasController : Controller
    {
        private readonly TestowyZKartki01Context _context;

        public PoziomZaawansowaniasController(TestowyZKartki01Context context)
        {
            _context = context;
        }

        // GET: PoziomZaawansowanias
        public async Task<IActionResult> Index()
        {
            return View(await _context.PoziomZaawansowaniaSet.ToListAsync());
        }

        // GET: PoziomZaawansowanias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poziomZaawansowania = await _context.PoziomZaawansowaniaSet
                .SingleOrDefaultAsync(m => m.ID == id);
            if (poziomZaawansowania == null)
            {
                return NotFound();
            }

            return View(poziomZaawansowania);
        }

        // GET: PoziomZaawansowanias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PoziomZaawansowanias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nazwa")] PoziomZaawansowania poziomZaawansowania)
        {
            if (ModelState.IsValid)
            {
                _context.Add(poziomZaawansowania);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(poziomZaawansowania);
        }

        // GET: PoziomZaawansowanias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poziomZaawansowania = await _context.PoziomZaawansowaniaSet.SingleOrDefaultAsync(m => m.ID == id);
            if (poziomZaawansowania == null)
            {
                return NotFound();
            }
            return View(poziomZaawansowania);
        }

        // POST: PoziomZaawansowanias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nazwa")] PoziomZaawansowania poziomZaawansowania)
        {
            if (id != poziomZaawansowania.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(poziomZaawansowania);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PoziomZaawansowaniaExists(poziomZaawansowania.ID))
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
            return View(poziomZaawansowania);
        }

        // GET: PoziomZaawansowanias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var poziomZaawansowania = await _context.PoziomZaawansowaniaSet
                .SingleOrDefaultAsync(m => m.ID == id);
            if (poziomZaawansowania == null)
            {
                return NotFound();
            }

            return View(poziomZaawansowania);
        }

        // POST: PoziomZaawansowanias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var poziomZaawansowania = await _context.PoziomZaawansowaniaSet.SingleOrDefaultAsync(m => m.ID == id);
            _context.PoziomZaawansowaniaSet.Remove(poziomZaawansowania);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PoziomZaawansowaniaExists(int id)
        {
            return _context.PoziomZaawansowaniaSet.Any(e => e.ID == id);
        }
    }
}
