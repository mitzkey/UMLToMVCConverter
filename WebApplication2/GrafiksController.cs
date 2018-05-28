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
    public class GrafiksController : Controller
    {
        private readonly TestowyZKartki01Context _context;

        public GrafiksController(TestowyZKartki01Context context)
        {
            _context = context;
        }

        // GET: Grafiks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Grafik.ToListAsync());
        }

        // GET: Grafiks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grafik = await _context.Grafik
                .SingleOrDefaultAsync(m => m.ID == id);
            if (grafik == null)
            {
                return NotFound();
            }

            return View(grafik);
        }

        // GET: Grafiks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Grafiks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Rok,Semestr")] Grafik grafik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grafik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(grafik);
        }

        // GET: Grafiks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grafik = await _context.Grafik.SingleOrDefaultAsync(m => m.ID == id);
            if (grafik == null)
            {
                return NotFound();
            }
            return View(grafik);
        }

        // POST: Grafiks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Rok,Semestr")] Grafik grafik)
        {
            if (id != grafik.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grafik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GrafikExists(grafik.ID))
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
            return View(grafik);
        }

        // GET: Grafiks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grafik = await _context.Grafik
                .SingleOrDefaultAsync(m => m.ID == id);
            if (grafik == null)
            {
                return NotFound();
            }

            return View(grafik);
        }

        // POST: Grafiks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grafik = await _context.Grafik.SingleOrDefaultAsync(m => m.ID == id);
            _context.Grafik.Remove(grafik);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GrafikExists(int id)
        {
            return _context.Grafik.Any(e => e.ID == id);
        }
    }
}
