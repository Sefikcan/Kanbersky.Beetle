using Kanbersky.Beetle.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kanbersky.Beetle.Infrastructure.EventStore.Abstract
{
    public interface IStore<T> where T : class, IEntity
    {
        Task Add(T entity);

        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate = null);

        IQueryable<T> GetQueryable();
    }
}
