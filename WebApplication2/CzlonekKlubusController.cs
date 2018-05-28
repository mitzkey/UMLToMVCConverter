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
    public class CzlonekKlubusController : Controller
    {
        private readonly TestowyZKartki01Context _context;

        public CzlonekKlubusController(TestowyZKartki01Context context)
        {
            _context = context;
        }

        // GET: CzlonekKlubus
        public async Task<IActionResult> Index()
        {
            var testowyZKartki01Context = _context.CzlonekKlubu.Include(c => c.WniosekPrzyjetyNaPodstawie);
            return View(await testowyZKartki01Context.ToListAsync());
        }

        // GET: CzlonekKlubus/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var czlonekKlubu = await _context.CzlonekKlubu
                .Include(c => c.WniosekPrzyjetyNaPodstawie)
                .SingleOrDefaultAsync(m => m.Pesel == id);
            if (czlonekKlubu == null)
            {
                return NotFound();
            }

            return View(czlonekKlubu);
        }

        // GET: CzlonekKlubus/Create
        public IActionResult Create()
        {
            ViewData["WniosekPrzyjetyNaPodstawiePesel"] = new SelectList(_context.Wniosek, "Pesel", "Pesel");
            return View();
        }

        // POST: CzlonekKlubus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WniosekPrzyjetyNaPodstawiePesel,Pesel")] CzlonekKlubu czlonekKlubu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(czlonekKlubu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WniosekPrzyjetyNaPodstawiePesel"] = new SelectList(_context.Wniosek, "Pesel", "Pesel", czlonekKlubu.WniosekPrzyjetyNaPodstawiePesel);
            return View(czlonekKlubu);
        }

        // GET: CzlonekKlubus/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var czlonekKlubu = await _context.CzlonekKlubu.SingleOrDefaultAsync(m => m.Pesel == id);
            if (czlonekKlubu == null)
            {
                return NotFound();
            }
            ViewData["WniosekPrzyjetyNaPodstawiePesel"] = new SelectList(_context.Wniosek, "Pesel", "Pesel", czlonekKlubu.WniosekPrzyjetyNaPodstawiePesel);
            return View(czlonekKlubu);
        }

        // POST: CzlonekKlubus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("WniosekPrzyjetyNaPodstawiePesel,Pesel")] CzlonekKlubu czlonekKlubu)
        {
            if (id != czlonekKlubu.Pesel)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(czlonekKlubu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CzlonekKlubuExists(czlonekKlubu.Pesel))
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
            ViewData["WniosekPrzyjetyNaPodstawiePesel"] = new SelectList(_context.Wniosek, "Pesel", "Pesel", czlonekKlubu.WniosekPrzyjetyNaPodstawiePesel);
            return View(czlonekKlubu);
        }

        // GET: CzlonekKlubus/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var czlonekKlubu = await _context.CzlonekKlubu
                .Include(c => c.WniosekPrzyjetyNaPodstawie)
                .SingleOrDefaultAsync(m => m.Pesel == id);
            if (czlonekKlubu == null)
            {
                return NotFound();
            }

            return View(czlonekKlubu);
        }

        // POST: CzlonekKlubus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var czlonekKlubu = await _context.CzlonekKlubu.SingleOrDefaultAsync(m => m.Pesel == id);
            _context.CzlonekKlubu.Remove(czlonekKlubu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CzlonekKlubuExists(string id)
        {
            return _context.CzlonekKlubu.Any(e => e.Pesel == id);
        }
    }
}
