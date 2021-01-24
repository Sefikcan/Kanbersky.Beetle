using Kanbersky.Beetle.Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kanbersky.Beetle.Infrastructure.Abstract
{
    public interface IMongoGenericRepository<TEntity> where TEntity : class, IEntity
    {
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, int pageIndex, int size);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, int pageIndex, int size);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, int pageIndex, int size, bool isDescending);

        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> order, int pageIndex, int size, bool isDescending);

        IEnumerable<TEntity> FindAll();

        Task<IEnumerable<TEntity>> FindAllAsync();

        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, object>> order, int pageIndex, int size);

        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, object>> order, int? pageIndex = 0, int? size = 10);

        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, object>> order, int pageIndex, int size, bool isDescending);

        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, object>> order, int pageIndex, int size, bool isDescending);

        bool Any(Expression<Func<TEntity, bool>> filter);

        bool Update<TField>(TEntity entity, Expression<Func<TEntity, TField>> field, TField value);

        Task<bool> UpdateAsync<TField>(TEntity entity, Expression<Func<TEntity, TField>> field, TField value);

        bool Update(TEntity entity, params UpdateDefinition<TEntity>[] updates);

        Task<bool> UpdateAsync(TEntity entity, params UpdateDefinition<TEntity>[] updates);

        bool Update<TField>(FilterDefinition<TEntity> filter, Expression<Func<TEntity, TField>> field, TField value);

        Task<bool> UpdateAsync<TField>(FilterDefinition<TEntity> filter, Expression<Func<TEntity, TField>> field, TField value);

        bool Update(FilterDefinition<TEntity> filter, params UpdateDefinition<TEntity>[] updates);

        Task<bool> UpdateAsync(FilterDefinition<TEntity> filter, params UpdateDefinition<TEntity>[] updates);

        bool Update(Expression<Func<TEntity, bool>> filter, params UpdateDefinition<TEntity>[] updates);

        Task<bool> UpdateAsync(Expression<Func<TEntity, bool>> filter, params UpdateDefinition<TEntity>[] updates);

        void Delete(Expression<Func<TEntity, bool>> filter);

        Task DeleteAsync(Expression<Func<TEntity, bool>> filter);

        void Insert(TEntity entity);

        Task InsertAsync(TEntity entity);

        void Insert(IEnumerable<TEntity> entities);

        Task InsertAsync(IEnumerable<TEntity> entities);
    }
}
