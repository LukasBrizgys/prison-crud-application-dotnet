using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EgzaminoProjektas.Models;
using EgzaminoProjektas.Repositories;
using Newtonsoft.Json;

namespace EgzaminoProjektas.Controllers
{
    public class PrisonerCrimesController : Controller
    {
        private readonly prisondbContext _context;
        private readonly IPrisonerCrimeRepository _prisonerCrimeRepository;

        public PrisonerCrimesController(prisondbContext context)
        {
            _context = context;
            _prisonerCrimeRepository = new PrisonerCrimeRepository(_context);
        }

        // GET: PrisonerCrimes
        public async Task<IActionResult> Index()
        {
            var prisonersCrimes = await _prisonerCrimeRepository.GetAllPrisonersCrimes();
            return View(prisonersCrimes);
        }

        // GET: PrisonerCrimes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Prisonercrimes == null)
            {
                return NotFound();
            }

            var prisonerCrime = await _context.Prisonercrimes
                .Include(p => p.Crime)
                .Include(p => p.Prisoner)
                .FirstOrDefaultAsync(m => m.CrimeId == id);
            if (prisonerCrime == null)
            {
                return NotFound();
            }

            return View(prisonerCrime);
        }

        // GET: PrisonerCrimes/Create
        public IActionResult Create()
        {
            ViewData["CrimeId"] = new SelectList(_context.Crimes, "Id", "Name");
            ViewData["PrisonerId"] = new SelectList((from s in _context.Prisoners.ToList() select new
            {
                s.Id, Fullname =s.Id + " - " + s.Name + " " + s.Surname
            }),
            "Id",
            "Fullname"
            );
            return View();
        }

        // POST: PrisonerCrimes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CrimeId,Date,PrisonerId")] PrisonerCrime prisonerCrime)
        {

            System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(ModelState));
            if (ModelState.IsValid)
            {
                await _context.Prisonercrimes.AddAsync(prisonerCrime);
                await _prisonerCrimeRepository.Save();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["CrimeId"] = new SelectList(_context.Crimes, "Id", "Name", prisonerCrime.CrimeId);
            ViewData["PrisonerId"] = new SelectList((from s in _context.Prisoners.ToList() select new
            {
                s.Id, Fullname =s.Id + " - " + s.Name + " " + s.Surname
            }),
            "Id",
            "Fullname"
            );
            return View(prisonerCrime);
        }

        // GET: PrisonerCrimes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Prisonercrimes == null)
            {
                return NotFound();
            }

            var prisonerCrime = await _context.Prisonercrimes.FindAsync(id);
            if (prisonerCrime == null)
            {
                return NotFound();
            }
            ViewData["CrimeId"] = new SelectList(_context.Crimes, "Id", "Id", prisonerCrime.CrimeId);
            ViewData["PrisonerId"] = new SelectList(_context.Prisoners, "Id", "Id", prisonerCrime.PrisonerId);
            return View(prisonerCrime);
        }

        // POST: PrisonerCrimes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("CrimeId,Date,PrisonerId")] PrisonerCrime prisonerCrime)
        {
            if (id != prisonerCrime.CrimeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prisonerCrime);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrisonerCrimeExists(prisonerCrime.CrimeId))
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
            ViewData["CrimeId"] = new SelectList(_context.Crimes, "Id", "Id", prisonerCrime.CrimeId);
            ViewData["PrisonerId"] = new SelectList(_context.Prisoners, "Id", "Id", prisonerCrime.PrisonerId);
            return View(prisonerCrime);
        }

        // GET: PrisonerCrimes/Delete/5
        public async Task<IActionResult> Delete(long? prisonerId, long? crimeId)
        {
            if (prisonerId == null || crimeId == null || _context.Prisonercrimes == null)
            {
                return NotFound();
            }

            await _prisonerCrimeRepository.DeletePrisonerCrime((long) prisonerId,(long) crimeId);
            await _prisonerCrimeRepository.Save();

            return RedirectToAction(nameof(Index));
        }

        // POST: PrisonerCrimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Prisonercrimes == null)
            {
                return Problem("Entity set 'prisondbContext.Prisonercrimes'  is null.");
            }
            var prisonerCrime = await _context.Prisonercrimes.FindAsync(id);
            if (prisonerCrime != null)
            {
                _context.Prisonercrimes.Remove(prisonerCrime);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEndpoint(long? prisonerId, long? crimeId)
        {
            if (prisonerId == null || crimeId == null) return NotFound();
            try
            {
                await _prisonerCrimeRepository.DeletePrisonerCrime((long) prisonerId,(long) crimeId);
                await _prisonerCrimeRepository.Save();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }


        }
        private bool PrisonerCrimeExists(long id)
        {
          return _context.Prisonercrimes.Any(e => e.CrimeId == id);
        }
    }
}
