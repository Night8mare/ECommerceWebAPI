namespace CleanArchEcommerce.Application.Common.Exceptions
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public string Error { get; private set; }
        public T Value { get; private set; }
        public ErrorType ErrorType { get; }

        public bool IsFailure => !IsSuccess;

        private Result(bool isSuccess, T value, string error, ErrorType errorType)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
            ErrorType = errorType;
        }

        public static Result<T> Success(T value) => new(true, value, null, ErrorType.None);

        public static Result<T> Failure(T value,string error, ErrorType errorType = ErrorType.BadRequest) => new(false, value , error, errorType);
    }
    public enum ErrorType
    {
        None,
        NotFound,
        Validation,
        Conflict,
        Unauthorized,
        Forbidden,
        BadRequest
    }
}
