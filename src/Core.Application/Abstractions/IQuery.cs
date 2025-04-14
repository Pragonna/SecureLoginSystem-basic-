using Core.Application.Common;
using MediatR;
using Sprache;

namespace Core.Application.Abstractions
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse, Error>> where TResponse : class, new()
    {
    }

    public interface IQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, Result<TResponse, Error>>
        where TRequest : IQuery<TResponse>
        where TResponse : class, new()
    {
    }
}
