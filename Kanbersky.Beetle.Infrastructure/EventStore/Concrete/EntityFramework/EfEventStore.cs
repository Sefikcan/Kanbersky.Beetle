﻿using Kanbersky.Beetle.Core.Entities;
using Kanbersky.Beetle.Infrastructure.EventStore.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

namespace Kanbersky.Beetle.Infrastructure.EventStore.Concrete.EntityFramework
{
    public class EfEventStore<T> : IStore<T> where T : class, IEntity
    {
        private readonly EventStoreEfDbContext _context;

        public EfEventStore(EventStoreEfDbContext context)
        {
            _context = context;
        }

        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (predicate != null)
                query = query.Where(predicate);

            return await query.AsNoTracking().ToListAsync();
        }

        public IQueryable<T> GetQueryable()
        {
            return _context.Set<T>().AsQueryable();
        }
    }
}
