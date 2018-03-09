using CryptoManager.Domain.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CryptoManager.Domain.Contracts.Repositories
{
    public interface IORM<T> where T : IEntity
    {
        IQueryable<T> GetManyWithoutDisable(Expression<Func<T, bool>> where);
        IQueryable<T> GetMany(Expression<Func<T, bool>> where);
        Task<List<T>> GetAllWithoutDisable();
        Task<List<T>> GetAll();
        Task<T> GetAsync(Guid id, bool loadFull = false);
        Task<T> InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
