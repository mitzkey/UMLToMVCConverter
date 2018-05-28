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
    public class WyposażenieController : Controller
    {
        private readonly TestowyZKartki01Context _context;

        public WyposażenieController(TestowyZKartki01Context context)
        {
            _context = context;
        }

        // GET: Wyposażenie
        public async Task<IActionResult> Index()
        {
            return View(await _context.Wyposażenie.ToListAsync());
        }

        // GET: Wyposażenie/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wyposażenie = await _context.Wyposażenie
                .SingleOrDefaultAsync(m => m.ID == id);
            if (wyposażenie == null)
            {
                return NotFound();
            }

            return View(wyposażenie);
        }

        // GET: Wyposażenie/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Wyposażenie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Koszt")] Wyposażenie wyposażenie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wyposażenie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wyposażenie);
        }

        // GET: Wyposażenie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wyposażenie = await _context.Wyposażenie.SingleOrDefaultAsync(m => m.ID == id);
            if (wyposażenie == null)
            {
                return NotFound();
            }
            return View(wyposażenie);
        }

        // POST: Wyposażenie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Koszt")] Wyposażenie wyposażenie)
        {
            if (id != wyposażenie.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wyposażenie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WyposażenieExists(wyposażenie.ID))
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
            return View(wyposażenie);
        }

        // GET: Wyposażenie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wyposażenie = await _context.Wyposażenie
                .SingleOrDefaultAsync(m => m.ID == id);
            if (wyposażenie == null)
            {
                return NotFound();
            }

            return View(wyposażenie);
        }

        // POST: Wyposażenie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wyposażenie = await _context.Wyposażenie.SingleOrDefaultAsync(m => m.ID == id);
            _context.Wyposażenie.Remove(wyposażenie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WyposażenieExists(int id)
        {
            return _context.Wyposażenie.Any(e => e.ID == id);
        }
    }
}
