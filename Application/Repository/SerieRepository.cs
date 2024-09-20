

using Database.Contexts;
using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository
{
    public class SerieRepository
    {
        private readonly ApplicationContext _context;

        public SerieRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Series series)
        {
            await _context.Series.AddAsync(series);
            await _context.SaveChangesAsync();
        }

        public async Task UdapteAsync(Series series)
        {
            _context.Series.Entry(series).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Series series)
        {
            _context.Set<Series>().Remove(series);
            await _context.SaveChangesAsync();
        }

        //public async Task<List<Series>> GetAllAsync()
        //{
        //    return await _context.Set<Series>().ToListAsync();
        //}
        public IQueryable<Series> GetAll()
        {
            return _context.Series.AsQueryable();
        }
        public async Task<Series> GetByIdAsync(int id)
        {
            return await _context.Set<Series>().FindAsync(id);
        }


    }
}
