﻿using GreenPipes;
using Kanbersky.Beetle.Core.Settings.Concrete.MessageBrokers;
using MassTransit;
using MassTransit.RabbitMqTransport;
using System;

namespace Kanbersky.Beetle.Core.MessageBrokers.Concrete.RabbitMQ.MassTransit
{
    public class BusConfigurator
    {
        //ihtiyaç duyulduğunda nesne örneği yaratmak için Lazy<T> sınıfını kullandık
        //Lazy Initialization: bellekte fazla yer kaplayacak bir nesneyi en baştan yaratmak yerine o nesneye ihtiyacımız olacağı yerde yaratmaya denir.
        private static readonly Lazy<BusConfigurator> configurator = new Lazy<BusConfigurator>(() => new BusConfigurator());

        public static BusConfigurator Instance => configurator.Value;

        public IBusControl ConfigureBus(MassTransitSettings massTransitSettings,
            Action<IRabbitMqBusFactoryConfigurator> action = null)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri(massTransitSettings.Uri), hst =>
                 {
                     hst.Username(massTransitSettings.UserName);
                     hst.Password(massTransitSettings.Password);
                 });

                if (massTransitSettings.MessageRetryCount.HasValue)
                    cfg.UseMessageRetry(u => u.Immediate(massTransitSettings.MessageRetryCount.Value));

                if(massTransitSettings.TripThreshold.HasValue && massTransitSettings.ActiveThreshold.HasValue && massTransitSettings.ResetInterval.HasValue)
                {
                    cfg.UseCircuitBreaker(cb =>
                    {
                        cb.TrackingPeriod = TimeSpan.FromMinutes(massTransitSettings.TrackingPeriod.Value);//TrackingPeriod ise ResetInterval süresinden sonra belirlediğimiz süre daha tetikte beklemesi söylüyor.Tekrar hata alınması durumunda ActiveThreshold ve TripThreshold limitlerini beklemeden yine resetinterval'da belirlenen süre ile beklemeye geçecektir.
                        cb.TripThreshold = massTransitSettings.TripThreshold.Value; // Alınan taleplerimizin belirlediğimiz % oranında hata olması durumunda restart olmasını sağlar.
                        cb.ActiveThreshold = massTransitSettings.ActiveThreshold.Value; // Üst üste belirlediğimiz miktarda hata aldığımızda restart olmasını sağlar
                    });
                }

                if (massTransitSettings.RateLimit.HasValue && massTransitSettings.RateLimiterInterval.HasValue)
                {
                    cfg.UseRateLimit(massTransitSettings.RateLimit.Value, TimeSpan.FromSeconds(massTransitSettings.RateLimiterInterval.Value)); // servise belirlediğimiz süre içerisinde belirlenen request adeti kadar istek yapabilecek şekilde yapıyorum.
                }

                action?.Invoke(cfg);
            });
        }
    }
}