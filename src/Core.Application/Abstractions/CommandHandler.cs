using Core.Application.Common;
using Core.Application.Repositories;
using Core.Domain.Exceptions;
using EventBus.EventBus.Base.Abstraction;
using System.Net;

namespace Core.Application.Abstractions
{
    public abstract class CommandHandler<TRequest, TResponse>(
        IUnitOfWork unitOfWork,
        IEventBus eventBus = null) : ICommandHandler<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
        where TResponse : class, new()
    {

        public async Task<Result<TResponse, Error>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var response = await ExecuteAsync((TRequest)request, cancellationToken);
            if (!response.IsSuccess)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            await unitOfWork.CommitAsync();

            if (response.HasEvent)
                await DispatchEventAsync(response, cancellationToken);

            return response;
        }
        public abstract Task<Result<TResponse, Error>> ExecuteAsync(TRequest request, CancellationToken cancellationToken);
        private async Task DispatchEventAsync(Result<TResponse, Error> result, CancellationToken cancellationToken)
        {
            if (eventBus == null) throw new ArgumentNullException(nameof(eventBus));

            foreach (var @event in result.Events)
            {
                await eventBus.Publish(@event);
            }
        }

    }
}
