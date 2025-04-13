using EventBus.EventBus.Base;
using EventBus.EventBus.Base.Abstraction;
using EventBus.EventBus.Base.Enums;
using EventBus.Events;
using EventBus.Factory;
using EventBus.RabbitMQ;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using test.unit.tests.Events;

namespace test.unit.tests
{
    public class EventBus
    {
        private readonly ServiceCollection _services;
        private readonly EventBusConfig config;
        public EventBus()
        {
            _services = new ServiceCollection();
            config = new EventBusConfig()
            {
                ConnectionRetryCount = 5,
                EventNameSuffix = "IntegrationEvent",
                SubscriberClientAppName = "default-client-name",
                DefaultTopicName = "default-topic-name",
                EventBusType = EventBusType.RabbitMQ,
                EventBusConnectionString = "amqps://xnovqftg:lto9JsLnOIdXTkms3ZKwxcu7cFpF1qph@fly.rmq.cloudamqp.com/xnovqftg"
            };
        }
        [Fact]
        public async Task subscriber_has_consume_when_publish()
        {
            var mockHandler = new Mock<IIntegrationEventHandler<SenderIntegrationEvent>>();

            mockHandler.Setup(mh => mh.Handle(It.IsAny<SenderIntegrationEvent>()))
                .Returns(Task.CompletedTask)
                .Callback<SenderIntegrationEvent>(@event =>
                {
                    @event.message.Should().Be("true");
                });

            _services.AddSingleton<IEventBus>(provider =>
           {
               return EventBusFactory.Create(config, provider);
           });

            _services.AddSingleton(mockHandler.Object);
       

            var sp = _services.BuildServiceProvider();
            var eventBus = sp.GetRequiredService<IEventBus>();

            await eventBus.Subscribe<SenderIntegrationEvent, IIntegrationEventHandler<SenderIntegrationEvent>>();
            await eventBus.Publish(new SenderIntegrationEvent("true"));

            await Task.Delay(1000);

            //eventBus.Should().NotBeNull();
            mockHandler.Verify(x => x.Handle(It.Is<SenderIntegrationEvent>(e => e.message == "true")), Times.Once);

        }
    }
}
