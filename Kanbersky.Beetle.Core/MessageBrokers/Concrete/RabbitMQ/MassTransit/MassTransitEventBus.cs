using Kanbersky.Beetle.Core.MessageBrokers.Abstract;
using Kanbersky.Beetle.Core.MessageBrokers.Models;
using Kanbersky.Beetle.Core.Settings.Concrete.MessageBrokers;
using MassTransit;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Kanbersky.Beetle.Core.MessageBrokers.Concrete.RabbitMQ.MassTransit
{
    public class MassTransitEventBus : IEventBus
    {
        private IBusControl _bus;
        private readonly MassTransitSettings _massTransitSettings;
        private readonly IServiceProvider _serviceProvider;

        public MassTransitEventBus(MassTransitSettings massTransitSettings,
            IServiceProvider serviceProvider)
        {
            _massTransitSettings = massTransitSettings;
            _serviceProvider = serviceProvider;
            _bus = BusConfigurator.Instance.ConfigureBus(_massTransitSettings);
        }

        public Task Publish<T>(T model) where T : EventModel
        {
            return _bus.Publish(model);
        }

        public async Task Subscribe<T, THandler>()
            where T : EventModel
            where THandler : IEventHandler<T>
        {
            if (_bus == null)
                throw new Exception("Masstransit could not be initialized!");

            var queueName = typeof(T).Name;
            _bus = BusConfigurator.Instance.ConfigureBus(_massTransitSettings, (cfg) =>
            {
                cfg.ReceiveEndpoint(queueName, e =>
                {
                    //Farklı exchange tipleri ile çalışmak için kullanılır
                    //e.AutoDelete = true;
                    //e.Durable = false;
                    //e.ExchangeType = ExchangeType.Direct;
                    //e.Bind("exchange-name");
                    //e.Bind<T>();
                    e.Consumer(() => 
                    {
                        var handler = _serviceProvider.GetService<IEventHandler<T>>();
                        return new MassTransitAdapter<T>(handler);
                    });
                });
            });

            await _bus.StartAsync();
        }

        public async Task UnSubscribe<T, THandler>()
            where T : EventModel
            where THandler : IEventHandler<T>
        {
            if (_bus == null)
                throw new Exception("Masstransit could not be initialized!");

            await _bus.StopAsync();
            _bus = null;
        }
    }
}
