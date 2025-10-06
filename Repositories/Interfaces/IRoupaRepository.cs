using SempreBella.Model;

namespace SempreBella.Repositories.Interfaces
{
    public interface IRoupaRepository : IGenericRepository<Roupa>
    {
        Task<bool> ExistsAsync(int id);
    }
}
