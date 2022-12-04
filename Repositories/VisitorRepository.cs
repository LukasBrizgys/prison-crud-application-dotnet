using EgzaminoProjektas.Models;
using Microsoft.EntityFrameworkCore;

namespace EgzaminoProjektas.Repositories
{
    public class VisitorRepository : IVisitorRepository
    {
        private readonly prisondbContext _context;
        private bool disposed = false;

        public VisitorRepository() { }
        public VisitorRepository(prisondbContext context)
        {
            _context = context;
        }
        public async Task<List<Visitor>> GetVisitors()
        {
            List < Visitor > visitors = await _context.Visitors.ToListAsync();
            return visitors;
        }
        public async Task<Visitor> GetVisitor(long visitorId)
        {
            Visitor? visitor = await _context.Visitors.FindAsync(visitorId);
            return visitor;
        }
        public List<PrisonerVisitor> GetVisitorPrisoners(long visitorId)
        {
            var prisoners = (from pv in _context.Prisonervisitors
                            join p in _context.Prisoners on pv.PrisonerId equals p.Id
                            join v in _context.Visitors on pv.VisitorId equals v.Id
                            where pv.VisitorId == visitorId
                            select pv).Include(v => v.Visitor).Include(p => p.Prisoner).ToList();
            return prisoners;
        }
        public void CreateVisitor(Visitor visitor)
        {
            _context.Visitors.Add(visitor);
        }
        public void UpdateVisitor(Visitor visitor)
        {
            _context.Visitors.Update(visitor);
        }
        public async Task DeleteVisitor(long visitorId)
        {
            Visitor? visitor = await _context.Visitors.FindAsync(visitorId);
            _context.Visitors.Remove(visitor);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
    public interface IVisitorRepository : IDisposable
    {
        public Task<List<Visitor>> GetVisitors();
        public Task<Visitor> GetVisitor(long visitorId);
        void CreateVisitor(Visitor visitor);
        void UpdateVisitor(Visitor visitor);
        Task DeleteVisitor(long visitorId);

        List<PrisonerVisitor> GetVisitorPrisoners(long visitorId);
        Task Save();
    }
}
