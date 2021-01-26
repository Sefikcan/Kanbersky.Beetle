using Kanbersky.Beetle.Core.Entities;
using Kanbersky.Beetle.Core.Settings.Concrete.Infrastructure.MongoDb;
using Kanbersky.Beetle.Infrastructure.EventStore.Abstract;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Kanbersky.Beetle.Infrastructure.EventStore.Concrete.MongoDb
{
    public class MongoDbStore<T> : IStore<T> where T : class, IEntity
    {
        private readonly IMongoCollection<T> _collection;

        public MongoDbStore(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionStrings);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _collection = CreateOrGetCollection(database, settings.Value.CollectionName);
        }

        private IMongoCollection<T> CreateOrGetCollection(IMongoDatabase database, string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);
            var collection = database.ListCollections(new ListCollectionsOptions { Filter = filter });

            var collectionIsExists = collection.Any();
            if (!collectionIsExists)
                database.CreateCollection(collectionName);

            return database.GetCollection<T>(collectionName);
        }

        public async Task Add(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            var query = await _collection.FindAsync(predicate);
            return query.ToEnumerable();
        }

        public IQueryable<T> GetQueryable()
        {
            return _collection.AsQueryable();
        }
    }
}
