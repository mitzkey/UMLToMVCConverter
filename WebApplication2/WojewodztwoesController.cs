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
    public class WojewodztwoesController : Controller
    {
        private readonly TestowyZKartki01Context _context;

        public WojewodztwoesController(TestowyZKartki01Context context)
        {
            _context = context;
        }

        // GET: Wojewodztwoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.WojewodztwoSet.ToListAsync());
        }

        // GET: Wojewodztwoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wojewodztwo = await _context.WojewodztwoSet
                .SingleOrDefaultAsync(m => m.ID == id);
            if (wojewodztwo == null)
            {
                return NotFound();
            }

            return View(wojewodztwo);
        }

        // GET: Wojewodztwoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Wojewodztwoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nazwa,Aktualna")] Wojewodztwo wojewodztwo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wojewodztwo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wojewodztwo);
        }

        // GET: Wojewodztwoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wojewodztwo = await _context.WojewodztwoSet.SingleOrDefaultAsync(m => m.ID == id);
            if (wojewodztwo == null)
            {
                return NotFound();
            }
            return View(wojewodztwo);
        }

        // POST: Wojewodztwoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nazwa,Aktualna")] Wojewodztwo wojewodztwo)
        {
            if (id != wojewodztwo.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wojewodztwo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WojewodztwoExists(wojewodztwo.ID))
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
            return View(wojewodztwo);
        }

        // GET: Wojewodztwoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wojewodztwo = await _context.WojewodztwoSet
                .SingleOrDefaultAsync(m => m.ID == id);
            if (wojewodztwo == null)
            {
                return NotFound();
            }

            return View(wojewodztwo);
        }

        // POST: Wojewodztwoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wojewodztwo = await _context.WojewodztwoSet.SingleOrDefaultAsync(m => m.ID == id);
            _context.WojewodztwoSet.Remove(wojewodztwo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WojewodztwoExists(int id)
        {
            return _context.WojewodztwoSet.Any(e => e.ID == id);
        }
    }
}
