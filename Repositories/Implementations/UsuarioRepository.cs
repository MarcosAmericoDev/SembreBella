using Microsoft.EntityFrameworkCore;
using SempreBella.Data;
using SempreBella.Model;
using SempreBella.Repositories.Interfaces;

namespace SempreBella.Repositories.Implementations
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> GetByEmailAndSenhaAsync(string email, string senhaHash)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == email && x.SenhaHash == senhaHash);
        }
    }
}
