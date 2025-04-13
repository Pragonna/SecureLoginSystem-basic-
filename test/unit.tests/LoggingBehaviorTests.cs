using Core.Application.Common;
using FluentAssertions;
using MediatR;
using Moq;

namespace test.unit.tests
{
    public class LoggingBehaviorTests
    {
        [Fact]
        public async Task Should_Log_Error_Message_When_Result_Is_Failure()
        {
            // Arrange
            var loggerMock = new Mock<Serilog.ILogger>();
            var loggingBehavior = new LoggingBehavior<FakeRequest, Result<FakeResponse, FakeError>>();

            var request = new FakeRequest();
            var handler = new FakeHandler();

            RequestHandlerDelegate<Result<FakeResponse, FakeError>> next = () => handler.Handle(request, default);

            // Act
            var result = await loggingBehavior.Handle(request, next, default);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.ErrorMessage.Should().Be("Test error message");
        }
    }
}
