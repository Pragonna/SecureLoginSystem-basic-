using Core.Application.Common;
using MediatR;
using Serilog;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly Serilog.ILogger _logger;

    public LoggingBehavior()
    {
        _logger = Log.ForContext<LoggingBehavior<TRequest, TResponse>>();
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        _logger.Information("Handling request: {RequestName}, Data: {@Request}", requestName, request);

        try
        {
            var response = await next(); // continue to the next handler in the pipeline
            _logger.Information("Request {RequestName} handled successfully.", requestName);

            if (response is IResult<Error> resultWithError && !resultWithError.IsSuccess && resultWithError.Error != null)
            {
                _logger.Warning("Request {RequestName} returned error: {ErrorMessage}", requestName, resultWithError.Error.ErrorMessage);
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Request {RequestName} failed.", requestName);
            throw;
        }
    }
}
