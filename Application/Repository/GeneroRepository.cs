using Database.Contexts;
using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository
{
    public class GeneroRepository
    {
        private readonly ApplicationContext _context;

        public GeneroRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Genero genero)
        {
            await _context.Generos.AddAsync(genero);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Genero genero)
        {
            _context.Generos.Entry(genero).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Genero genero)
        {
            _context.Set<Genero>().Remove(genero);
            await _context.SaveChangesAsync();
        }
        public IQueryable<Genero> GetAll()
        {
            return _context.Generos.AsQueryable();
        }
        public async Task<Genero> GetByIdAsync(int id)
        {
            return await _context.Set<Genero>().FindAsync(id);
        }
    }
}
