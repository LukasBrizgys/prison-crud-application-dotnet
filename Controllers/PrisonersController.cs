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
using EgzaminoProjektas.Enums;
using System.Security.Principal;

namespace EgzaminoProjektas.Controllers
{
    public class PrisonersController : Controller
    {
        private readonly prisondbContext _context;
        private readonly IPrisonerRepository _prisonerRepository;
        private readonly IPrisonerCrimeRepository _prisonerCrimeRepository;

        public PrisonersController(prisondbContext context)
        {
            _context = context;
            _prisonerRepository = new PrisonerRepository(_context);
        }

        // GET: Prisoners
        public async Task<IActionResult> Index()
        {
            var prisoners = await _prisonerRepository.GetPrisoners();
            return View(prisoners);
        }

        // GET: Prisoners/Details/5
        public IActionResult Visitors(long? id)
        {
            if (id == null || _context.Prisoners == null)
            {
                return NotFound();
            }

            var prisonerVisitors = _prisonerRepository.GetPrisonerVisitors((long)id);

            return View(prisonerVisitors);
        }
        public IActionResult Crimes(long? id)
        {
            if(id == null || _context.Prisoners == null)
            {
                return NotFound();
            }
            var prisonerCrimes = _prisonerRepository.GetPrisonerCrimes((long)id);
            return View(prisonerCrimes);
        }
        // GET: Prisoners/Create
        public IActionResult Create()
        {
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
            return View();
        }

        // POST: Prisoners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BirthDate,Name,Phone,Surname,CityId,StatusId,FileName")] Prisoner prisoner)
        {
            if (ModelState.IsValid)
            {
                
                try
                {
                    _prisonerRepository.CreatePrisoner(prisoner);
                    await _prisonerRepository.Save();
                }catch(DataException)
                {
                    ModelState.AddModelError(string.Empty, "Nepavyko išsaugoti");
                }
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", prisoner.CityId);
            return View(prisoner);
        }

        // GET: Prisoners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Prisoners == null)
            {
                return NotFound();
            }

            var prisoner = await _prisonerRepository.GetPrisoner((int) id);
            if (prisoner == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", prisoner.CityId);
            return View(prisoner);
        }

        // POST: Prisoners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BirthDate,Name,Phone,Surname,CityId,StatusId,FileName")] Prisoner prisoner)
        {
            if (id != prisoner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _prisonerRepository.UpdatePrisoner(prisoner);
                   await _prisonerRepository.Save();
                }catch(DataException)
                {
                    ModelState.AddModelError(string.Empty, "Nepavyko išsaugoti");
                }
               
               
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", prisoner.CityId);
            return RedirectToAction(nameof(Index));
        }

        // GET: Prisoners/Delete/5
        public async Task<IActionResult> Delete(bool? saveChangesError, int? id)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Nepavyko ištrinti";
            }
            if (id == null || _context.Prisoners == null)
            {
                return NotFound();
            }

            var prisoner = await _context.Prisoners
                .Include(p => p.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prisoner == null)
            {
                return NotFound();
            }

            return View(prisoner);
        }

        // POST: Prisoners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _prisonerRepository.DeletePrisoner(id);
                await _prisonerRepository.Save();
            }
            catch (DataException)
            {

            }
            return RedirectToAction(nameof(Index));
        }

        private bool PrisonerExists(int id)
        {
          return _context.Prisoners.Any(e => e.Id == id);
        }
        protected override void Dispose(bool disposing)
        {
            _prisonerRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
