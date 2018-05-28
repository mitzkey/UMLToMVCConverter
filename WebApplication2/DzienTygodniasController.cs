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
    public class DzienTygodniasController : Controller
    {
        private readonly TestowyZKartki01Context _context;

        public DzienTygodniasController(TestowyZKartki01Context context)
        {
            _context = context;
        }

        // GET: DzienTygodnias
        public async Task<IActionResult> Index()
        {
            return View(await _context.DzienTygodnia.ToListAsync());
        }

        // GET: DzienTygodnias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dzienTygodnia = await _context.DzienTygodnia
                .SingleOrDefaultAsync(m => m.ID == id);
            if (dzienTygodnia == null)
            {
                return NotFound();
            }

            return View(dzienTygodnia);
        }

        // GET: DzienTygodnias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DzienTygodnias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] DzienTygodnia dzienTygodnia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dzienTygodnia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dzienTygodnia);
        }

        // GET: DzienTygodnias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dzienTygodnia = await _context.DzienTygodnia.SingleOrDefaultAsync(m => m.ID == id);
            if (dzienTygodnia == null)
            {
                return NotFound();
            }
            return View(dzienTygodnia);
        }

        // POST: DzienTygodnias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] DzienTygodnia dzienTygodnia)
        {
            if (id != dzienTygodnia.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dzienTygodnia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DzienTygodniaExists(dzienTygodnia.ID))
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
            return View(dzienTygodnia);
        }

        // GET: DzienTygodnias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dzienTygodnia = await _context.DzienTygodnia
                .SingleOrDefaultAsync(m => m.ID == id);
            if (dzienTygodnia == null)
            {
                return NotFound();
            }

            return View(dzienTygodnia);
        }

        // POST: DzienTygodnias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dzienTygodnia = await _context.DzienTygodnia.SingleOrDefaultAsync(m => m.ID == id);
            _context.DzienTygodnia.Remove(dzienTygodnia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DzienTygodniaExists(int id)
        {
            return _context.DzienTygodnia.Any(e => e.ID == id);
        }
    }
}
