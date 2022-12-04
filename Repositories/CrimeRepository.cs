using EgzaminoProjektas.Models;
using Microsoft.EntityFrameworkCore;

namespace EgzaminoProjektas.Repositories
{
    public class CrimeRepository : ICrimeRepository
    {
        private readonly prisondbContext _context;
        private bool disposed = false;
        public CrimeRepository() { }
        public CrimeRepository(prisondbContext context)
        {
            _context = context;
        }
        public async Task<List<Crime>> GetAllCrimes()
        {
            var crimes = await _context.Crimes.ToListAsync();
            return crimes;
        }
        public async Task<Crime?> GetCrime(long crimeId)
        {
            Crime crime = await _context.Crimes.FirstOrDefaultAsync(c => c.Id == crimeId);
            return crime;
        }
        public void CreateCrime(Crime crime)
        {
            _context.Crimes.Add(crime);
        }
        public async Task DeleteCrime(long crimeId)
        {
            Crime crime = await _context.Crimes.FindAsync(crimeId);
            _context.Crimes.Remove(crime);
        }
        public void UpdateCrime(Crime crime)
        {
            _context.Update(crime);
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
    public interface ICrimeRepository : IDisposable
    {
        Task<List<Crime>> GetAllCrimes();
        Task<Crime?> GetCrime(long crimeId);
        void CreateCrime(Crime crime);
        void UpdateCrime(Crime crime);
        Task DeleteCrime(long crimeId);
        Task Save();

    }

}
