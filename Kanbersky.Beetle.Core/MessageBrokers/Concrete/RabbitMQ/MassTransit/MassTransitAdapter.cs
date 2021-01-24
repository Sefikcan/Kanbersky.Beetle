using Kanbersky.Beetle.Core.MessageBrokers.Abstract;
using Kanbersky.Beetle.Core.MessageBrokers.Models;
using MassTransit;
using System.Threading.Tasks;

namespace Kanbersky.Beetle.Core.MessageBrokers.Concrete.RabbitMQ.MassTransit
{
    public class MassTransitAdapter<T> : IConsumer<T> where T : EventModel
    {
        private readonly IEventHandler<T> _eventHandler;

        public MassTransitAdapter(IEventHandler<T> eventHandler)
        {
            _eventHandler = eventHandler;
        }

        public async Task Consume(ConsumeContext<T> context)
        {
            await _eventHandler.Handle(context.Message);
        }
    }
}
