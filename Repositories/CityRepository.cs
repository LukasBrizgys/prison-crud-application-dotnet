using EgzaminoProjektas.Models;
using Microsoft.EntityFrameworkCore;

namespace EgzaminoProjektas.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly prisondbContext _context;
        private bool disposed = false;
        public CityRepository(prisondbContext context)
        {
            _context = context;
        }
        public async Task<List<City>> GetCities()
        {
            List<City> cities = await _context.Cities.ToListAsync();
            return cities;
        }

        public async Task<City?> GetCity(byte cityId)
        {
           City? city = await _context.Cities.FirstOrDefaultAsync(m => m.Id == cityId);
            return city;
        }
        public async Task CreateCity(City city)
        {
            await _context.Cities.AddAsync(city);
         
        }
        public void UpdateCity(City city)
        {
                _context.Cities.Update(city);
        }
        public async Task DeleteCity(byte cityId)
        {
            var city = await _context.Cities.FindAsync(cityId);
            _context.Cities.Remove(city);
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
    public interface ICityRepository : IDisposable
    {
        Task<List<City>> GetCities();
        Task<City?> GetCity(byte id);
        Task CreateCity(City city);
        void UpdateCity(City city);
        Task DeleteCity(byte id);
        Task Save();
    }
}
