using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchEcommerce.Application.Common.Exceptions
{
    public class MetaResult <T>
    {
        public bool IsSuccess { get; private set; }
        public string Error { get; private set; }
        public T Value { get; private set; }
        public ErrorType ErrorType { get; }
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }
        public bool IsFailure => !IsSuccess;

        private MetaResult(bool isSuccess, T value, string error, ErrorType errorType, int pageNumber, int totalPages)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
            ErrorType = errorType;
            PageNumber = pageNumber;
            TotalPages = totalPages;
        }

        public static MetaResult<T> Success(T value, int pageNumber, int totalPages) => new(true, value, null, ErrorType.None, pageNumber, totalPages);

        public static MetaResult<T> Failure(T value, string error, int pageNumber, int totalPages, ErrorType errorType = ErrorType.BadRequest) => 
            new(false, value, error, errorType, pageNumber, totalPages);
    }
}
