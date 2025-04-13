using Core.Application.Common;
using MediatR;

namespace Core.Application.Abstractions
{
    public interface ICommand<TResponse> : IRequest<Result<TResponse, Error>>
        where TResponse : class , new()
    {
    }

    public interface ICommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, Result<TResponse, Error>>
        where TRequest : ICommand<TResponse>
        where TResponse : class, new()
    {
    }
}
