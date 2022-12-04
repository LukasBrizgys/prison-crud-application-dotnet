using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EgzaminoProjektas.Models;
using EgzaminoProjektas.Repositories;
using System.Data;

namespace EgzaminoProjektas.Controllers
{
    public class CrimesController : Controller
    {
        private readonly prisondbContext _context;
        private readonly ICrimeRepository _crimeRepository;

        public CrimesController(prisondbContext context)
        {
            _context = context;
            _crimeRepository = new CrimeRepository(_context);
        }

        // GET: Crimes
        public async Task<IActionResult> Index()
        {
            List<Crime> crimes = await _crimeRepository.GetAllCrimes();
              return View(crimes);
        }

        // GET: Crimes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Crimes == null)
            {
                return NotFound();
            }

            Crime crime = await _crimeRepository.GetCrime((long) id);
            if (crime == null)
            {
                return NotFound();
            }

            return View(crime);
        }

        // GET: Crimes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Crimes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Crime crime)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _crimeRepository.CreateCrime(crime);
                    await _crimeRepository.Save();
                }catch(DataException)
                {
                    ModelState.AddModelError(string.Empty, "Nepavyko išsaugoti");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(crime);
        }

        // GET: Crimes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Crimes == null)
            {
                return NotFound();
            }

            var crime = await _context.Crimes.FindAsync(id);
            if (crime == null)
            {
                return NotFound();
            }
            return View(crime);
        }

        // POST: Crimes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name")] Crime crime)
        {
            if (id != crime.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _crimeRepository.UpdateCrime(crime);
                    await _crimeRepository.Save();
                }
                catch (DataException)
                {
                    if (!CrimeExists(crime.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Nepavyko išsaugoti");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(crime);
        }

        // GET: Crimes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Crimes == null)
            {
                return NotFound();
            }

            var crime = await _context.Crimes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crime == null)
            {
                return NotFound();
            }

            return View(crime);
        }

        // POST: Crimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Crimes == null)
            {
                return Problem("Entity set 'prisondbContext.Crimes'  is null.");
            }
            try
            {
                await _crimeRepository.DeleteCrime(id);
                await _crimeRepository.Save();
            }
            catch (DataException)
            {
                ModelState.AddModelError(string.Empty, "Nepavyko išsaugoti");
            }
            return RedirectToAction(nameof(Index));
        }
        protected override void Dispose(bool disposing)
        {
            _crimeRepository.Dispose();
            base.Dispose(disposing);
        }
        private bool CrimeExists(long id)
        {
          return _context.Crimes.Any(e => e.Id == id);
        }
    }
}
