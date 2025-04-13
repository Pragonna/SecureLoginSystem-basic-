namespace Core.Application.Common
{
    public interface IResult <TError> where TError : IError
    {
        TError Error { get; }
        bool IsSuccess { get; }
    }
}