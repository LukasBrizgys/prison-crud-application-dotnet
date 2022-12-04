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
    public class VisitorsController : Controller
    {
        private readonly prisondbContext _context;
        private readonly VisitorRepository _visitorRepository;

        public VisitorsController(prisondbContext context)
        {
            _context = context;
            _visitorRepository = new(_context);
        }

        // GET: Visitors
        public async Task<IActionResult> Index()
        {
              return View(await _context.Visitors.ToListAsync());
        }

        // GET: Visitors/Details/5
        public IActionResult Details(long? id)
        {
            if (id == null || _context.Visitors == null)
            {
                return NotFound();
            }

            var visitors = _visitorRepository.GetVisitorPrisoners((long) id);

            return View(visitors);
        }

        // GET: Visitors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Visitors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BirthDate,Name,Surname")] Visitor visitor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(visitor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(visitor);
            }
            catch (DBConcurrencyException)
            {
                ModelState.AddModelError(String.Empty, "Nepavyko pridėti");
                return View(visitor);
            }
            
        }

        // GET: Visitors/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Visitors == null)
            {
                return NotFound();
            }

            var visitor = await _context.Visitors.FindAsync(id);
            if (visitor == null)
            {
                return NotFound();
            }
            return View(visitor);
        }

        // POST: Visitors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,BirthDate,Name,Surname")] Visitor visitor)
        {
            if (id != visitor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visitor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisitorExists(visitor.Id))
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
            return View(visitor);
        }

        // GET: Visitors/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Visitors == null)
            {
                return NotFound();
            }

            var visitor = await _context.Visitors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visitor == null)
            {
                return NotFound();
            }

            return View(visitor);
        }

        // POST: Visitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Visitors == null)
            {
                return Problem("Entity set 'prisondbContext.Visitors'  is null.");
            }
            var visitor = await _context.Visitors.FindAsync(id);
            if (visitor != null)
            {
                _context.Visitors.Remove(visitor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisitorExists(long id)
        {
          return _context.Visitors.Any(e => e.Id == id);
        }
    }
}
