using Core.Application.Behaviors.Validations;
using EventBus.EventBus.Base;
using EventBus.EventBus.Base.Abstraction;
using EventBus.Factory;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, EventBusConfig config)
        {
            services.AddSingleton<IEventBus>(provider =>
            {
                return EventBusFactory.Create(config, provider);
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
