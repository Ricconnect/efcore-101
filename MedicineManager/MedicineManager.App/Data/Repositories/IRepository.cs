using MedicineManager.App.Data.Models;

namespace MedicineManager.App.Data.Repositories;

/// <summary>
/// Generic repository interface for basic CRUD operations.
/// This abstraction allows us to swap out the persistence mechanism
/// (e.g., file system → Entity Framework Core) without changing the application code.
/// </summary>
public interface IRepository<T> where T : class, IEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
}
