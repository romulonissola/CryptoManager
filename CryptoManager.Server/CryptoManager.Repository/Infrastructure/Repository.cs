using CryptoManager.Domain.Contracts.Entities;
using CryptoManager.Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CryptoManager.Repository.Infrastructure
{
    public abstract class Repository<T> : IRepository<T> where T : IEntity
    {
        internal readonly IORM<T> _ORM;
        public Repository(IORM<T> orm)
        {
            _ORM = orm;
        }

        public Task DeleteAsync(T entity)
        {
            return _ORM.DeleteAsync(entity);
        }

        public Task<T> GetAsync(Guid id)
        {
            return _ORM.GetAsync(id);
        }

        public Task<List<T>> GetAllWithoutDisable()
        {
            return _ORM.GetAllWithoutDisable();
        }

        public Task<List<T>> GetAll()
        {
            return _ORM.GetAll();
        }

        public virtual Task<T> InsertAsync(T entity)
        {
            return _ORM.InsertAsync(entity);
        }

        public virtual Task UpdateAsync(T entity)
        {
            return _ORM.UpdateAsync(entity);
        }
    }
}
