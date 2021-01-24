using Kanbersky.Beetle.Core.Entities;
using Kanbersky.Beetle.Infrastructure.EventStore.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kanbersky.Beetle.Infrastructure.EventStore.Concrete.MongoDb
{
    public class MongoDbStore<T> : IStore<T> where T : class, IEntity
    {
        public Task Add(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
