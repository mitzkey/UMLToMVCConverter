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
    public class WnioseksController : Controller
    {
        private readonly TestowyZKartki01Context _context;

        public WnioseksController(TestowyZKartki01Context context)
        {
            _context = context;
        }

        // GET: Wnioseks
        public async Task<IActionResult> Index()
        {
            var testowyZKartki01Context = _context.Wniosek.Include(w => w.AdresDoKorespondencji).Include(w => w.AdresZameldowania).Include(w => w.Status);
            return View(await testowyZKartki01Context.ToListAsync());
        }

        // GET: Wnioseks/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wniosek = await _context.Wniosek
                .Include(w => w.AdresDoKorespondencji)
                .Include(w => w.AdresZameldowania)
                .Include(w => w.Status)
                .SingleOrDefaultAsync(m => m.Pesel == id);
            if (wniosek == null)
            {
                return NotFound();
            }

            return View(wniosek);
        }

        // GET: Wnioseks/Create
        public IActionResult Create()
        {
            ViewData["AdresDoKorespondencjiID"] = new SelectList(_context.Adres, "ID", "ID");
            ViewData["AdresZameldowaniaID"] = new SelectList(_context.Adres, "ID", "ID");
            ViewData["StatusID"] = new SelectList(_context.StatusWniosku, "ID", "ID");
            return View();
        }

        // POST: Wnioseks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DataZlozenia,DataRozpatrzenia,Pesel,StatusID,AdresZameldowaniaID,AdresDoKorespondencjiID")] Wniosek wniosek)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wniosek);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdresDoKorespondencjiID"] = new SelectList(_context.Adres, "ID", "ID", wniosek.AdresDoKorespondencjiID);
            ViewData["AdresZameldowaniaID"] = new SelectList(_context.Adres, "ID", "ID", wniosek.AdresZameldowaniaID);
            ViewData["StatusID"] = new SelectList(_context.StatusWniosku, "ID", "ID", wniosek.StatusID);
            return View(wniosek);
        }

        // GET: Wnioseks/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wniosek = await _context.Wniosek.SingleOrDefaultAsync(m => m.Pesel == id);
            if (wniosek == null)
            {
                return NotFound();
            }
            ViewData["AdresDoKorespondencjiID"] = new SelectList(_context.Adres, "ID", "ID", wniosek.AdresDoKorespondencjiID);
            ViewData["AdresZameldowaniaID"] = new SelectList(_context.Adres, "ID", "ID", wniosek.AdresZameldowaniaID);
            ViewData["StatusID"] = new SelectList(_context.StatusWniosku, "ID", "ID", wniosek.StatusID);
            return View(wniosek);
        }

        // POST: Wnioseks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("DataZlozenia,DataRozpatrzenia,Pesel,StatusID,AdresZameldowaniaID,AdresDoKorespondencjiID")] Wniosek wniosek)
        {
            if (id != wniosek.Pesel)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wniosek);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WniosekExists(wniosek.Pesel))
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
            ViewData["AdresDoKorespondencjiID"] = new SelectList(_context.Adres, "ID", "ID", wniosek.AdresDoKorespondencjiID);
            ViewData["AdresZameldowaniaID"] = new SelectList(_context.Adres, "ID", "ID", wniosek.AdresZameldowaniaID);
            ViewData["StatusID"] = new SelectList(_context.StatusWniosku, "ID", "ID", wniosek.StatusID);
            return View(wniosek);
        }

        // GET: Wnioseks/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wniosek = await _context.Wniosek
                .Include(w => w.AdresDoKorespondencji)
                .Include(w => w.AdresZameldowania)
                .Include(w => w.Status)
                .SingleOrDefaultAsync(m => m.Pesel == id);
            if (wniosek == null)
            {
                return NotFound();
            }

            return View(wniosek);
        }

        // POST: Wnioseks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var wniosek = await _context.Wniosek.SingleOrDefaultAsync(m => m.Pesel == id);
            _context.Wniosek.Remove(wniosek);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WniosekExists(string id)
        {
            return _context.Wniosek.Any(e => e.Pesel == id);
        }
    }
}
