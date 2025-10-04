using SempreBella.Model;

namespace SempreBella.Services
{
    public interface IRoupaService
    {
        Task<List<Roupa>> GetAllAsync();
        Task<Roupa?> GetByIdAsync(int id);
        Task<bool> ExistsAsync(int id);

        Task CreateAsync(Roupa roupa);
        Task UpdateAsync(Roupa roupa);
        Task DeleteAsync(int id);
    }
}
