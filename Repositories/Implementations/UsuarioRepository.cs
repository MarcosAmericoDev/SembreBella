using Microsoft.EntityFrameworkCore;
using SempreBella.Data;
using SempreBella.Model;
using SempreBella.Repositories.Interfaces;

namespace SempreBella.Repositories.Implementations
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Usuario?> GetByEmailAndSenhaAsync(string email, string senhaHash)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == email && x.SenhaHash == senhaHash);
        }
        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
