using EgzaminoProjektas.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EgzaminoProjektas.Repositories
{
    public class PrisonerRepository : IPrisonerRepository
    {
        private readonly prisondbContext _context;
        private Boolean disposed = false;
        public PrisonerRepository(prisondbContext context)
        {
            _context = context;
        }

        public async Task<List<Prisoner>> GetPrisoners()
        {
            var prisoners = await _context.Prisoners.Include(p => p.City).OrderBy(e => e.Name).ToListAsync();
            return prisoners;
        }
        public async Task<Prisoner?> GetPrisoner(long prisonerId)
        {
            var prisoner = await _context.Prisoners
                .Include(p => p.City)
                .FirstOrDefaultAsync(m => m.Id == prisonerId);
            return prisoner;
        }

        public void CreatePrisoner(Prisoner prisoner)
        {
            _context.Add(prisoner);
            
        }
        public void UpdatePrisoner(Prisoner prisoner)
        {
                _context.Update(prisoner);
        }
        public async Task DeletePrisoner(long prisonerId)
        {
                var prisoner = await _context.Prisoners.FindAsync(prisonerId);
            if (prisoner != null)
            {
                _context.Prisoners.Remove(prisoner);
            }
               
        }
        public List<PrisonerVisitor> GetPrisonerVisitors(long prisonerId)
        {
            var visitors = (from pv in _context.Prisonervisitors
                            join p in _context.Prisoners on pv.PrisonerId equals p.Id
                            join v in _context.Visitors on pv.VisitorId equals v.Id
                            where pv.PrisonerId == prisonerId
                            select pv).Include(v => v.Visitor).Include(p => p.Prisoner).ToList();
                                            
            return visitors;
        }
        public List<PrisonerCrime> GetPrisonerCrimes(long prisonerId)
        {
            var crimes = _context.Prisonercrimes.Include(c => c.Crime).Where(p => p.PrisonerId == prisonerId).Join(
                _context.Crimes,
                prisonerCrime => prisonerCrime.CrimeId,
                crime => crime.Id,
                (prisonerCrime, crimes) => new { prisonerCrime}
                ).Select(p => p.prisonerCrime).Join(
                    _context.Prisoners,
                    pc => pc.PrisonerId,
                    p => p.Id,
                    (pc, p) => new { pc}).Select(p => p.pc).ToList();
            return crimes;
        }
        public int GetTotalPrisonerCount()
        {
            int count = _context.Prisoners.Count();
            return count;
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

    public interface IPrisonerRepository : IDisposable
    {
        Task<List<Prisoner>> GetPrisoners();

        Task<Prisoner?> GetPrisoner(long prisonerId);

        void CreatePrisoner(Prisoner prisoner);

        Task DeletePrisoner(long prisonerId);

        void UpdatePrisoner(Prisoner prisoner);
        List<PrisonerVisitor> GetPrisonerVisitors(long prisonerId);
        List<PrisonerCrime> GetPrisonerCrimes(long prisonerId);
        int GetTotalPrisonerCount();

        Task Save();
    }
}
