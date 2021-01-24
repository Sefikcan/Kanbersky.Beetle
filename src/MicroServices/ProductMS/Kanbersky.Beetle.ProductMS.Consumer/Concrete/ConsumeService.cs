using Kanbersky.Beetle.Core.MessageBrokers.Abstract;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kanbersky.Beetle.ProductMS.Consumer.Concrete
{
    public class ConsumeService : IHostedService
    {
        private readonly IEventBus _eventBus;

        public ConsumeService(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
