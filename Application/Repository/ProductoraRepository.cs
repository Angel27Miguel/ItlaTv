using Database.Contexts;
using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository
{
    public class ProductoraRepository
    {
        private readonly ApplicationContext _context;

        public ProductoraRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Productora productora)
        {
            await _context.Productoras.AddAsync(productora);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Productora productora)
        {
            _context.Productoras.Entry(productora).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Productora productora)
        {
            _context.Set<Productora>().Remove(productora);
            await _context.SaveChangesAsync();
        }
        public IQueryable<Productora> GetAll()
        {
            return _context.Productoras.AsQueryable();
        }
        public async Task<Productora> GetByIdAsync(int id)
        {
            return await _context.Set<Productora>().FindAsync(id);
        }
    }
}

