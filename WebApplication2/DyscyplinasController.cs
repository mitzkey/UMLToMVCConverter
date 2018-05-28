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
    public class DyscyplinasController : Controller
    {
        private readonly TestowyZKartki01Context _context;

        public DyscyplinasController(TestowyZKartki01Context context)
        {
            _context = context;
        }

        // GET: Dyscyplinas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dyscyplina.ToListAsync());
        }

        // GET: Dyscyplinas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dyscyplina = await _context.Dyscyplina
                .SingleOrDefaultAsync(m => m.ID == id);
            if (dyscyplina == null)
            {
                return NotFound();
            }

            return View(dyscyplina);
        }

        // GET: Dyscyplinas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dyscyplinas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nazwa")] Dyscyplina dyscyplina)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dyscyplina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dyscyplina);
        }

        // GET: Dyscyplinas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dyscyplina = await _context.Dyscyplina.SingleOrDefaultAsync(m => m.ID == id);
            if (dyscyplina == null)
            {
                return NotFound();
            }
            return View(dyscyplina);
        }

        // POST: Dyscyplinas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nazwa")] Dyscyplina dyscyplina)
        {
            if (id != dyscyplina.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dyscyplina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DyscyplinaExists(dyscyplina.ID))
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
            return View(dyscyplina);
        }

        // GET: Dyscyplinas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dyscyplina = await _context.Dyscyplina
                .SingleOrDefaultAsync(m => m.ID == id);
            if (dyscyplina == null)
            {
                return NotFound();
            }

            return View(dyscyplina);
        }

        // POST: Dyscyplinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dyscyplina = await _context.Dyscyplina.SingleOrDefaultAsync(m => m.ID == id);
            _context.Dyscyplina.Remove(dyscyplina);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DyscyplinaExists(int id)
        {
            return _context.Dyscyplina.Any(e => e.ID == id);
        }
    }
}
