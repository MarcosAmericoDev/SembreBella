using Microsoft.EntityFrameworkCore;
using SempreBella.Model;

namespace SempreBella.Repositories.Interfaces
{
    public interface IUsuarioRepository : IGenericRepository<Usuario>
    {
        Task<Usuario?> GetByEmailAndSenhaAsync(string email, string senhaHash);
        Task<Usuario?> GetByEmailAsync(string email);
    }
}
