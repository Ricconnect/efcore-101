using MedicineManager.App.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicineManager.App.Data.Repositories
{
    public class EfCoreRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly MedicationDbContext context;

        public EfCoreRepository(MedicationDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(T entity)
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public Task DeleteAsync(Guid id)
        {
            var entity = context.Find<T>(id);

            if(entity != null)
            {
                context.Remove(entity);
                return context.SaveChangesAsync();
            }

            return Task.CompletedTask;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await context.FindAsync<T>(id);
        }

        public async Task UpdateAsync(T entity)
        {
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
