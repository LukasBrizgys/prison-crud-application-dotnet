using EgzaminoProjektas.Models;
using Microsoft.EntityFrameworkCore;

namespace EgzaminoProjektas.Repositories
{
    public class PrisonerCrimeRepository : IPrisonerCrimeRepository
    {
        private readonly prisondbContext _context;
        private bool disposed = false;
        public PrisonerCrimeRepository() { }
        public PrisonerCrimeRepository(prisondbContext context)
        {
            _context = context;
        }
        public async Task<List<PrisonerCrime>> GetAllPrisonersCrimes()
        {
            List<PrisonerCrime> prisonersCrimes = await _context.Prisonercrimes.Include(p => p.Prisoner).Include(c => c.Crime).ToListAsync();
            return prisonersCrimes;
        }
        public async Task<PrisonerCrime?> GetPrisonerCrime(long prisonerId, long crimeId)
        {
            PrisonerCrime? prisonerCrime = _context.Prisonercrimes.Include(c => c.Crime).Include(p => p.Prisoner).First(pc => pc.PrisonerId == prisonerId && pc.CrimeId == crimeId);
            return prisonerCrime;
        }
        public void CreatePrisonerCrime(PrisonerCrime prisonerCrime)
        {
            _context.Add(prisonerCrime);
        }
        public async Task DeletePrisonerCrime(long prisonerId, long crimeId)
        {
            PrisonerCrime? prisonerCrime = _context.Prisonercrimes.First(pc => pc.PrisonerId == prisonerId && pc.CrimeId == crimeId);
            if(prisonerCrime != null) _context.Prisonercrimes.Remove(prisonerCrime);
            
        }
        public void UpdatePrisonerCrime(PrisonerCrime prisonerCrime)
        {
            _context.Prisonercrimes.Update(prisonerCrime);
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

    public interface IPrisonerCrimeRepository : IDisposable
    {
        Task<List<PrisonerCrime>> GetAllPrisonersCrimes();
        Task<PrisonerCrime?> GetPrisonerCrime(long prisonerId, long crimeId);

        void CreatePrisonerCrime(PrisonerCrime prisonerCrime);

        Task DeletePrisonerCrime(long prisonerId, long crimeId);

        void UpdatePrisonerCrime(PrisonerCrime prisonerCrime);
        Task Save();

    }
}
