using Kanbersky.Beetle.Core.MessageBrokers.Abstract;
using Kanbersky.Beetle.Domain.Shared.MessageBrokers.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kanbersky.Beetle.ProductMS.Consumer.Consumers
{
    public class CreateProductConsumer : IEventHandler<CreateProductConsumerModel>
    {
        public Task Handle(CreateProductConsumerModel @event)
        {
            throw new NotImplementedException();
        }
    }
}
