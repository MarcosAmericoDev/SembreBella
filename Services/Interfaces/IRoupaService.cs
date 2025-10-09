using SempreBella.Model;
using SempreBella.ViewModels;

namespace SempreBella.Services.Interfaces
{
    public interface IRoupaService
    {
        Task<List<Roupa>> GetAllAsync();
        Task<List<RoupaExibicaoDTO>> GetAllAtivasAsync();
        Task<Roupa?> GetByIdAsync(int id);
        Task<bool> ExistsAsync(int id);

        Task CreateAsync(Roupa roupa);
        Task UpdateAsync(Roupa roupa);
        Task DeleteAsync(int id);
    }
}
