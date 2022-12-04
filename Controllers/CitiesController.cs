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
    public class CitiesController : Controller
    {
        private readonly prisondbContext _context;
        private readonly CityRepository _cityRepository;

        public CitiesController(prisondbContext context)
        {
            _context = context;
            _cityRepository = new CityRepository(_context);
            
        }

        // GET: Cities
        public async Task<IActionResult> Index()
        {
            List < City > cities = await _cityRepository.GetCities();
            return View(cities);
        }

        // GET: Cities/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null || _context.Cities == null)
            {
                return NotFound();
            }

            var city = await _cityRepository.GetCity((byte) id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // GET: Cities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Name")] City city)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _cityRepository.CreateCity(city);
                    await _cityRepository.Save();
                }
                catch (DataException)
                {
                    ModelState.AddModelError(string.Empty, "Nepavyko išsaugoti");
                    return View(city);
                }
            }

            return RedirectToAction(nameof(Index));
            
        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null || _context.Cities == null)
            {
                return NotFound();
            }

            var city = await _cityRepository.GetCity((byte) id);
            if (city == null)
            {
                return NotFound();
            }
            return View(city);
        }
        [HttpPut]
        public async Task<IActionResult> Update(City city)
        {
            try
            {
                City foundCity = await _cityRepository.GetCity(city.Id);
                if (!ModelState.IsValid) return NotFound();
                    foundCity.Name = city.Name;
                    _cityRepository.UpdateCity(foundCity);
                    await _cityRepository.Save();
                    return Ok();

            }catch(DataException)
            {
                return BadRequest();
            }
            
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEndpoint(byte cityId)
        {
            try
            {
                await _cityRepository.DeleteCity(cityId);
                await _cityRepository.Save();
                return Ok();
            }catch(Exception)
            {
                return BadRequest();
            }


        }
        // POST: Cities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("Id,Name")] City city)
        {
            if (id != city.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _cityRepository.UpdateCity(city);
                    await _cityRepository.Save();
                }
                catch (DataException)
                {
                    ModelState.AddModelError(string.Empty, "Nepavyko išsaugoti");
                }
            return RedirectToAction(nameof(Index));
            }
            return View(city);
        }

        // GET: Cities/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null || _context.Cities == null)
            {
                return NotFound();
            }

            var city = await _context.Cities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            if (_context.Cities == null)
            {
                return Problem("Entity set 'prisondbContext.Cities'  is null.");
            }
            try
            {
                await _cityRepository.DeleteCity(id);
                await _cityRepository.Save();
            }catch(DataException)
            {
                ModelState.AddModelError(string.Empty, "Nepavyko išsaugoti");
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        protected override void Dispose(bool disposing)
        {
            _cityRepository.Dispose();
            base.Dispose(disposing);
        }
        private bool CityExists(byte id)
        {
            return _context.Cities.Any(e => e.Id == id);
        }
    }
}
