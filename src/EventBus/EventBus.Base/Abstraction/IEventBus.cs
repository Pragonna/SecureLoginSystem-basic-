using EventBus.Events;

namespace EventBus.EventBus.Base.Abstraction
{
    public interface IEventBus
    {
        Task Publish(IntegrationEvent @event);
        Task Subscribe<T, THandle>() where T : IntegrationEvent where THandle : IIntegrationEventHandler<T>;
        Task UnSubscribe<T, THandle>() where T : IntegrationEvent where THandle : IIntegrationEventHandler<T>;
    }
}
