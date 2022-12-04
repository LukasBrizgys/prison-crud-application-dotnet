using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EgzaminoProjektas.Models;
using System.Data;

namespace EgzaminoProjektas.Controllers
{
    public class PrisonerVisitorsController : Controller
    {
        private readonly prisondbContext _context;

        public PrisonerVisitorsController(prisondbContext context)
        {
            _context = context;
        }

        // GET: PrisonerVisitors
        public async Task<IActionResult> Index()
        {
            var prisondbContext = _context.Prisonervisitors.Include(p => p.Prisoner).Include(p => p.Visitor);
            return View(await prisondbContext.ToListAsync());
        }

        // GET: PrisonerVisitors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Prisonervisitors == null)
            {
                return NotFound();
            }

            var prisonerVisitor = await _context.Prisonervisitors
                .Include(p => p.Prisoner)
                .Include(p => p.Visitor)
                .FirstOrDefaultAsync(m => m.PrisonerId == id);
            if (prisonerVisitor == null)
            {
                return NotFound();
            }

            return View(prisonerVisitor);
        }

        // GET: PrisonerVisitors/Create
        public IActionResult Create()
        {
            ViewData["PrisonerId"] = new SelectList(_context.Prisoners, "Id", "Id");
            ViewData["VisitorId"] = new SelectList(_context.Visitors, "Id", "Id");
            return View();
        }

        // POST: PrisonerVisitors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrisonerId,StartDate,VisitorId,FinishDate")] PrisonerVisitor prisonerVisitor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prisonerVisitor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PrisonerId"] = new SelectList(_context.Prisoners, "Id", "Id", prisonerVisitor.PrisonerId);
            ViewData["VisitorId"] = new SelectList(_context.Visitors, "Id", "Id", prisonerVisitor.VisitorId);
            return View(prisonerVisitor);
        }

        // GET: PrisonerVisitors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Prisonervisitors == null)
            {
                return NotFound();
            }

            var prisonerVisitor = await _context.Prisonervisitors.FindAsync(id);
            if (prisonerVisitor == null)
            {
                return NotFound();
            }
            ViewData["PrisonerId"] = new SelectList(_context.Prisoners, "Id", "Id", prisonerVisitor.PrisonerId);
            ViewData["VisitorId"] = new SelectList(_context.Visitors, "Id", "Id", prisonerVisitor.VisitorId);
            return View(prisonerVisitor);
        }

        // POST: PrisonerVisitors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrisonerId,StartDate,VisitorId,FinishDate")] PrisonerVisitor prisonerVisitor)
        {
            if (id != prisonerVisitor.PrisonerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prisonerVisitor);
                    await _context.SaveChangesAsync();
                }
                catch (DataException)
                {
                        return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PrisonerId"] = new SelectList(_context.Prisoners, "Id", "Id", prisonerVisitor.PrisonerId);
            ViewData["VisitorId"] = new SelectList(_context.Visitors, "Id", "Id", prisonerVisitor.VisitorId);
            return View(prisonerVisitor);
        }

        // GET: PrisonerVisitors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Prisonervisitors == null)
            {
                return NotFound();
            }

            var prisonerVisitor = await _context.Prisonervisitors
                .Include(p => p.Prisoner)
                .Include(p => p.Visitor)
                .FirstOrDefaultAsync(m => m.PrisonerId == id);
            if (prisonerVisitor == null)
            {
                return NotFound();
            }

            return View(prisonerVisitor);
        }

        // POST: PrisonerVisitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Prisonervisitors == null)
            {
                return Problem("Entity set 'prisondbContext.Prisonervisitors'  is null.");
            }
            var prisonerVisitor = await _context.Prisonervisitors.FindAsync(id);
            if (prisonerVisitor != null)
            {
                _context.Prisonervisitors.Remove(prisonerVisitor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrisonerVisitorExists(long id)
        {
          return _context.Prisonervisitors.Any(e => e.PrisonerId == id);
        }
    }
}
