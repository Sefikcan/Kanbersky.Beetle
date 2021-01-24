using Kanbersky.Beetle.Core.MessageBrokers.Models;
using System.Threading.Tasks;

namespace Kanbersky.Beetle.Core.MessageBrokers.Abstract
{
    public interface IEventBus
    {
        Task Publish<T>(T model) where T : EventModel;

        Task Subscribe<T, THandler>()
           where T : EventModel
           where THandler : IEventHandler<T>;

        Task UnSubscribe<T, THandler>()
            where T : EventModel
            where THandler : IEventHandler<T>;
    }
}
