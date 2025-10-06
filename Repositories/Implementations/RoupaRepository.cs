using Microsoft.EntityFrameworkCore;
using SempreBella.Data;
using SempreBella.Model;
using SempreBella.Repositories.Interfaces;
using System.Linq.Expressions;

namespace SempreBella.Repositories.Implementations
{
    public class RoupaRepository : GenericRepository<Roupa>, IRoupaRepository
    {
        public RoupaRepository(ApplicationDbContext context) : base(context)
        {
                
        }
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Roupas.AnyAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Roupa roupa) 
        {
            _context.Attach(roupa).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
