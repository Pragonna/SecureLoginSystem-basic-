using EventBus.Events;

namespace Core.Application.Common
{
    public class Result<TResponse, TError> : IResult<TError>
        where TResponse : class, new()
        where TError : IError
    {
        public TResponse Response { get; }
        public TError Error { get; }
        public bool IsSuccess { get; }
        public bool HasEvent { get; private set; }
        public ICollection<IntegrationEvent> Events { get; private set; }
        private Result(TResponse response, IntegrationEvent @event = null)
        {
            Response = response;
            IsSuccess = true;
            AddEvent(@event);
        }
        private Result(TError error, IntegrationEvent @event = null)
        {
            Error = error;
            IsSuccess = false;
            AddEvent(@event);
        }

        public static Result<TResponse, TError> Success(TResponse response, IntegrationEvent @event = null)
        {
            return new Result<TResponse, TError>(response, @event);
        }
        public static Result<TResponse, TError> Success(TResponse response, IList<IntegrationEvent> events)
        {
            var result = new Result<TResponse, TError>(response);
            if (events != null)
            {
                result.Events = events;
                result.HasEvent = true;
            }
            return result;
        }
        public static Result<TResponse, TError> Failure(TError error, IntegrationEvent @event = null)
        {
            return new Result<TResponse, TError>(error, @event);
        }
        public static Result<TResponse, TError> Failure(TError error, IList<IntegrationEvent> events)
        {
            var result = new Result<TResponse, TError>(error);
            if (events != null)
            {
                result.Events = events;
                result.HasEvent = true;
            }
            return result;
        }
        private void AddEvent(IntegrationEvent @event)
        {
            if (@event != null)
            {
                Events = Events ?? new HashSet<IntegrationEvent>();
                Events.Add(@event);
                HasEvent = true;
            }
        }
    }

}
