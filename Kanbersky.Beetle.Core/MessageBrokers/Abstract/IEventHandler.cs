using Kanbersky.Beetle.Core.MessageBrokers.Models;
using System.Threading.Tasks;

namespace Kanbersky.Beetle.Core.MessageBrokers.Abstract
{
    public interface IEventHandler<in TEvent> where TEvent : EventModel
    {
        Task Handle(TEvent @event);
    }
}
