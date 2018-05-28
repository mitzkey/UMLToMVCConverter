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
    public class ZajeciasController : Controller
    {
        private readonly TestowyZKartki01Context _context;

        public ZajeciasController(TestowyZKartki01Context context)
        {
            _context = context;
        }

        // GET: Zajecias
        public async Task<IActionResult> Index()
        {
            var testowyZKartki01Context = _context.Zajecia.Include(z => z.Kurs).Include(z => z.Sala).Include(z => z.Termin);
            return View(await testowyZKartki01Context.ToListAsync());
        }

        // GET: Zajecias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zajecia = await _context.Zajecia
                .Include(z => z.Kurs)
                .Include(z => z.Sala)
                .Include(z => z.Termin)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (zajecia == null)
            {
                return NotFound();
            }

            return View(zajecia);
        }

        // GET: Zajecias/Create
        public IActionResult Create()
        {
            ViewData["KursKod"] = new SelectList(_context.Kurs, "Kod", "Kod");
            ViewData["SalaID"] = new SelectList(_context.Sala, "ID", "ID");
            ViewData["TerminDzien"] = new SelectList(_context.Termin, "Dzien", "Dzien");
            return View();
        }

        // POST: Zajecias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,TerminDzien,TerminGodzinaRozpoczecia,KursKod,SalaID")] Zajecia zajecia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zajecia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KursKod"] = new SelectList(_context.Kurs, "Kod", "Kod", zajecia.KursKod);
            ViewData["SalaID"] = new SelectList(_context.Sala, "ID", "ID", zajecia.SalaID);
            ViewData["TerminDzien"] = new SelectList(_context.Termin, "Dzien", "Dzien", zajecia.TerminDzien);
            return View(zajecia);
        }

        // GET: Zajecias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zajecia = await _context.Zajecia.SingleOrDefaultAsync(m => m.ID == id);
            if (zajecia == null)
            {
                return NotFound();
            }
            ViewData["KursKod"] = new SelectList(_context.Kurs, "Kod", "Kod", zajecia.KursKod);
            ViewData["SalaID"] = new SelectList(_context.Sala, "ID", "ID", zajecia.SalaID);
            ViewData["TerminDzien"] = new SelectList(_context.Termin, "Dzien", "Dzien", zajecia.TerminDzien);
            return View(zajecia);
        }

        // POST: Zajecias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,TerminDzien,TerminGodzinaRozpoczecia,KursKod,SalaID")] Zajecia zajecia)
        {
            if (id != zajecia.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zajecia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZajeciaExists(zajecia.ID))
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
            ViewData["KursKod"] = new SelectList(_context.Kurs, "Kod", "Kod", zajecia.KursKod);
            ViewData["SalaID"] = new SelectList(_context.Sala, "ID", "ID", zajecia.SalaID);
            ViewData["TerminDzien"] = new SelectList(_context.Termin, "Dzien", "Dzien", zajecia.TerminDzien);
            return View(zajecia);
        }

        // GET: Zajecias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zajecia = await _context.Zajecia
                .Include(z => z.Kurs)
                .Include(z => z.Sala)
                .Include(z => z.Termin)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (zajecia == null)
            {
                return NotFound();
            }

            return View(zajecia);
        }

        // POST: Zajecias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zajecia = await _context.Zajecia.SingleOrDefaultAsync(m => m.ID == id);
            _context.Zajecia.Remove(zajecia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZajeciaExists(int id)
        {
            return _context.Zajecia.Any(e => e.ID == id);
        }
    }
}
