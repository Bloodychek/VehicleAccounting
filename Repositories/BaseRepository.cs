using Microsoft.EntityFrameworkCore;
using VehicleAccounting.Models;
using System.Linq.Expressions;

namespace VehicleAccounting.Repositories
{
    public class BaseRepository<T> : IRepository<T> 
        where T : class, IBaseObject
    {
        private readonly MainContext mainContext;

        public BaseRepository(MainContext mainContext)
        {
            this.mainContext = mainContext;
        }

        public async Task Create(T entity)
        {
            await mainContext.AddAsync(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            mainContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            var dbSet = mainContext.Set<T>().AsNoTracking();
            foreach (var include in includes)
            {
                dbSet = dbSet.Include(include);
            }

            return dbSet;
        }

        public async Task<T?> GetById(int id)
        {
            return await GetAll().FirstOrDefaultAsync(x => x.Id == id);
        }
                
        public async Task Save()
        {
            await mainContext.SaveChangesAsync();
        }
        
        public async Task Update(T entity)
        {
            mainContext.Update(entity);
        }
    }
}
