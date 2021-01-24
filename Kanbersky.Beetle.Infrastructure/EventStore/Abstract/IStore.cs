using Kanbersky.Beetle.Core.Entities;
using System.Threading.Tasks;

namespace Kanbersky.Beetle.Infrastructure.EventStore.Abstract
{
    public interface IStore<T> where T : class, IEntity
    {
        Task Add(T entity);
    }
}
