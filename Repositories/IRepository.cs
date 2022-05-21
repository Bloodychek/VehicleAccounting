using VehicleAccounting.Models;
using System.Linq.Expressions;

namespace VehicleAccounting.Repositories
{
    public interface IRepository<T>
        where T : IBaseObject
    {
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes);
        Task<T?> GetById(int id);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete (int id);
        Task Save();
    }
}
