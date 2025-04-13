using MediatR;

namespace Core.Application.Abstractions
{
    public interface IQuery<TResponse>:IRequest<TResponse>
    {
    }
    
    public interface IQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IQuery<TResponse>
    {
    }
}
