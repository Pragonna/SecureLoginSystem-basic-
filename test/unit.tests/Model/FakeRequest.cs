using Core.Application.Common;
using MediatR;

public class FakeRequest : IRequest<Result<FakeResponse, FakeError>> { }

public class FakeResponse { }

public class FakeError : IError
{
    public string ErrorMessage { get; set; }
    public FakeError(string message)
    {
        ErrorMessage = message;
    }
}

public class FakeHandler : IRequestHandler<FakeRequest, Result<FakeResponse, FakeError>>
{
    public Task<Result<FakeResponse, FakeError>> Handle(FakeRequest request, CancellationToken cancellationToken)
    {
        var error = new FakeError("Test error message");
        return Task.FromResult(Result<FakeResponse, FakeError>.Failure(error));
    }
}

