using Microsoft.EntityFrameworkCore;
using SempreBella.Data;
using SempreBella.Model;

namespace SempreBella.Services
{
    public class RoupaService : IRoupaService
    {
        private readonly ApplicationDbContext _context;

        public RoupaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task CreateAsync(Roupa roupa)
        {
            _context.Roupas.Add(roupa);
            return _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var roupa = await _context.Roupas.FindAsync(id);
            if(roupa != null)
            {
                _context.Roupas.Remove(roupa);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Roupas.AnyAsync(x => x.Id == id);
        }

        public async Task<List<Roupa>> GetAllAsync()
        {
            return await _context.Roupas.ToListAsync();
        }

        public async Task<Roupa?> GetByIdAsync(int id)
        {
            return await _context.Roupas.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task UpdateAsync(Roupa roupa)
        {
            _context.Attach(roupa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException)
            {
                if(!await ExistsAsync(roupa.Id)) {
                    throw new KeyNotFoundException($"Roua com ID {roupa.Id} não encontrada");
                }
                throw;
            }
        }
    }
}
