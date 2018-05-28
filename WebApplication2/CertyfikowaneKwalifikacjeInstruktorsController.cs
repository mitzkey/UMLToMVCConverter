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
    public class CertyfikowaneKwalifikacjeInstruktorsController : Controller
    {
        private readonly TestowyZKartki01Context _context;

        public CertyfikowaneKwalifikacjeInstruktorsController(TestowyZKartki01Context context)
        {
            _context = context;
        }

        // GET: CertyfikowaneKwalifikacjeInstruktors
        public async Task<IActionResult> Index()
        {
            var testowyZKartki01Context = _context.CertyfikowaneKwalifikacjeInstruktor.Include(c => c.CertyfikowaneKwalifikacje);
            return View(await testowyZKartki01Context.ToListAsync());
        }

        // GET: CertyfikowaneKwalifikacjeInstruktors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certyfikowaneKwalifikacjeInstruktor = await _context.CertyfikowaneKwalifikacjeInstruktor
                .Include(c => c.CertyfikowaneKwalifikacje)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (certyfikowaneKwalifikacjeInstruktor == null)
            {
                return NotFound();
            }

            return View(certyfikowaneKwalifikacjeInstruktor);
        }

        // GET: CertyfikowaneKwalifikacjeInstruktors/Create
        public IActionResult Create()
        {
            ViewData["CertyfikowaneKwalifikacjeID"] = new SelectList(_context.DyscyplinaZPoziomem, "ID", "ID");
            return View();
        }

        // POST: CertyfikowaneKwalifikacjeInstruktors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CertyfikowaneKwalifikacjeID")] CertyfikowaneKwalifikacjeInstruktor certyfikowaneKwalifikacjeInstruktor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(certyfikowaneKwalifikacjeInstruktor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CertyfikowaneKwalifikacjeID"] = new SelectList(_context.DyscyplinaZPoziomem, "ID", "ID", certyfikowaneKwalifikacjeInstruktor.CertyfikowaneKwalifikacjeID);
            return View(certyfikowaneKwalifikacjeInstruktor);
        }

        // GET: CertyfikowaneKwalifikacjeInstruktors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certyfikowaneKwalifikacjeInstruktor = await _context.CertyfikowaneKwalifikacjeInstruktor.SingleOrDefaultAsync(m => m.ID == id);
            if (certyfikowaneKwalifikacjeInstruktor == null)
            {
                return NotFound();
            }
            ViewData["CertyfikowaneKwalifikacjeID"] = new SelectList(_context.DyscyplinaZPoziomem, "ID", "ID", certyfikowaneKwalifikacjeInstruktor.CertyfikowaneKwalifikacjeID);
            return View(certyfikowaneKwalifikacjeInstruktor);
        }

        // POST: CertyfikowaneKwalifikacjeInstruktors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CertyfikowaneKwalifikacjeID")] CertyfikowaneKwalifikacjeInstruktor certyfikowaneKwalifikacjeInstruktor)
        {
            if (id != certyfikowaneKwalifikacjeInstruktor.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(certyfikowaneKwalifikacjeInstruktor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CertyfikowaneKwalifikacjeInstruktorExists(certyfikowaneKwalifikacjeInstruktor.ID))
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
            ViewData["CertyfikowaneKwalifikacjeID"] = new SelectList(_context.DyscyplinaZPoziomem, "ID", "ID", certyfikowaneKwalifikacjeInstruktor.CertyfikowaneKwalifikacjeID);
            return View(certyfikowaneKwalifikacjeInstruktor);
        }

        // GET: CertyfikowaneKwalifikacjeInstruktors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certyfikowaneKwalifikacjeInstruktor = await _context.CertyfikowaneKwalifikacjeInstruktor
                .Include(c => c.CertyfikowaneKwalifikacje)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (certyfikowaneKwalifikacjeInstruktor == null)
            {
                return NotFound();
            }

            return View(certyfikowaneKwalifikacjeInstruktor);
        }

        // POST: CertyfikowaneKwalifikacjeInstruktors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var certyfikowaneKwalifikacjeInstruktor = await _context.CertyfikowaneKwalifikacjeInstruktor.SingleOrDefaultAsync(m => m.ID == id);
            _context.CertyfikowaneKwalifikacjeInstruktor.Remove(certyfikowaneKwalifikacjeInstruktor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CertyfikowaneKwalifikacjeInstruktorExists(int id)
        {
            return _context.CertyfikowaneKwalifikacjeInstruktor.Any(e => e.ID == id);
        }
    }
}
