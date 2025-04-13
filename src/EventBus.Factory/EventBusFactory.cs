using EventBus.EventBus.Base;
using EventBus.EventBus.Base.Abstraction;
using EventBus.EventBus.Base.Enums;
using EventBus.RabbitMQ;

namespace EventBus.Factory
{
    public class EventBusFactory
    {
        public static IEventBus Create(EventBusConfig config, IServiceProvider provider)
        {
            return config.EventBusType switch
            {
                EventBusType.RabbitMQ => new EventBusRabbitMQ(provider, config)
            };
        }

    }
}
