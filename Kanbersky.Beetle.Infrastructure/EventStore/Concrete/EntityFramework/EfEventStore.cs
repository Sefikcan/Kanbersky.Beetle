using Kanbersky.Beetle.Core.Entities;
using Kanbersky.Beetle.Infrastructure.EventStore.Abstract;
using System.Threading.Tasks;

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
    }
}
