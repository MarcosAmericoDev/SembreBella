using SempreBella.Model;

namespace SempreBella.Services.Interfaces
{
    public interface IRoupaService
    {
        Task<List<Roupa>> GetAllAsync();
        Task<List<Roupa>> GetAllAtivasAsync();
        Task<Roupa?> GetByIdAsync(int id);
        Task<bool> ExistsAsync(int id);

        Task CreateAsync(Roupa roupa);
        Task UpdateAsync(Roupa roupa);
        Task DeleteAsync(int id);
    }
}
