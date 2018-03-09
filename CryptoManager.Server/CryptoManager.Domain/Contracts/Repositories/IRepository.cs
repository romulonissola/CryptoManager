using CryptoManager.Domain.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CryptoManager.Domain.Contracts.Repositories
{
    public interface IRepository<T> where T : IEntity
    {
        Task DeleteAsync(T entity);
        Task<T> GetAsync(Guid id);
        Task<List<T>> GetAllWithoutDisable();
        Task<List<T>> GetAll();
        Task<T> InsertAsync(T entity);
        Task UpdateAsync(T entity);
    }
}
