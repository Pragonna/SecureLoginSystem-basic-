using EmailSender.Service;
using EmailSender.Service.Events;
using EventBus.EventBus.Base.Abstraction;
using EventBus.Factory;
using EventBus.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;

ServiceCollection services = new ServiceCollection();

services.AddSingleton<SendVerificationEmailIntegrationEventHandler>();
services.AddSingleton<IEmailSender, EmailSender.Service.EmailService>();
services.AddSingleton<IEventBus>(provider =>
{
    return EventBusFactory.Create(RabbitMQOptions.GetConfigRabbitMQ, provider);
});

IServiceProvider serviceProvider = services.BuildServiceProvider();
IEventBus eventBus = serviceProvider.GetRequiredService<IEventBus>();

await eventBus.Subscribe<SendVerificationEmailIntegrationEvent, SendVerificationEmailIntegrationEventHandler>();

Console.Read();
