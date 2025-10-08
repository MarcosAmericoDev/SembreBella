using System.Linq.Expressions;

namespace SempreBella.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<List<T>> FindAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task Remove (T entity);
        Task<int> SaveChangesAsync();
    }
}
