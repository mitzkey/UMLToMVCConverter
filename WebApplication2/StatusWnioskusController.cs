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
    public class StatusWnioskusController : Controller
    {
        private readonly TestowyZKartki01Context _context;

        public StatusWnioskusController(TestowyZKartki01Context context)
        {
            _context = context;
        }

        // GET: StatusWnioskus
        public async Task<IActionResult> Index()
        {
            return View(await _context.StatusWniosku.ToListAsync());
        }

        // GET: StatusWnioskus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusWniosku = await _context.StatusWniosku
                .SingleOrDefaultAsync(m => m.ID == id);
            if (statusWniosku == null)
            {
                return NotFound();
            }

            return View(statusWniosku);
        }

        // GET: StatusWnioskus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StatusWnioskus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] StatusWniosku statusWniosku)
        {
            if (ModelState.IsValid)
            {
                _context.Add(statusWniosku);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(statusWniosku);
        }

        // GET: StatusWnioskus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusWniosku = await _context.StatusWniosku.SingleOrDefaultAsync(m => m.ID == id);
            if (statusWniosku == null)
            {
                return NotFound();
            }
            return View(statusWniosku);
        }

        // POST: StatusWnioskus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] StatusWniosku statusWniosku)
        {
            if (id != statusWniosku.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statusWniosku);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusWnioskuExists(statusWniosku.ID))
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
            return View(statusWniosku);
        }

        // GET: StatusWnioskus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusWniosku = await _context.StatusWniosku
                .SingleOrDefaultAsync(m => m.ID == id);
            if (statusWniosku == null)
            {
                return NotFound();
            }

            return View(statusWniosku);
        }

        // POST: StatusWnioskus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var statusWniosku = await _context.StatusWniosku.SingleOrDefaultAsync(m => m.ID == id);
            _context.StatusWniosku.Remove(statusWniosku);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatusWnioskuExists(int id)
        {
            return _context.StatusWniosku.Any(e => e.ID == id);
        }
    }
}
