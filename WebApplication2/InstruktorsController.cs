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
    public class InstruktorsController : Controller
    {
        private readonly TestowyZKartki01Context _context;

        public InstruktorsController(TestowyZKartki01Context context)
        {
            _context = context;
        }

        // GET: Instruktors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Instruktor.ToListAsync());
        }

        // GET: Instruktors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instruktor = await _context.Instruktor
                .SingleOrDefaultAsync(m => m.ID == id);
            if (instruktor == null)
            {
                return NotFound();
            }

            return View(instruktor);
        }

        // GET: Instruktors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Instruktors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID")] Instruktor instruktor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(instruktor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(instruktor);
        }

        // GET: Instruktors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instruktor = await _context.Instruktor.SingleOrDefaultAsync(m => m.ID == id);
            if (instruktor == null)
            {
                return NotFound();
            }
            return View(instruktor);
        }

        // POST: Instruktors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID")] Instruktor instruktor)
        {
            if (id != instruktor.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instruktor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstruktorExists(instruktor.ID))
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
            return View(instruktor);
        }

        // GET: Instruktors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instruktor = await _context.Instruktor
                .SingleOrDefaultAsync(m => m.ID == id);
            if (instruktor == null)
            {
                return NotFound();
            }

            return View(instruktor);
        }

        // POST: Instruktors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instruktor = await _context.Instruktor.SingleOrDefaultAsync(m => m.ID == id);
            _context.Instruktor.Remove(instruktor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstruktorExists(int id)
        {
            return _context.Instruktor.Any(e => e.ID == id);
        }
    }
}
