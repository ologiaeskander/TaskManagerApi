using System.Linq.Expressions;

namespace TaskManagerApi.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        // Get by ID
        Task<T?> GetByIdAsync(int id);

        // Get all with optional filtering, ordering, and including related entities
        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "");

        // Add
        Task AddAsync(T entity);

        // Update
        void Update(T entity);

        // Delete
        Task DeleteAsync(int id);
        void Delete(T entity);

        // Save changes
        Task<bool> SaveChangesAsync();
    }
}