using SempreBella.Model;

namespace SempreBella.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetByEmailAndSenhaAsync(string email, string senhaHash);
    }
}
