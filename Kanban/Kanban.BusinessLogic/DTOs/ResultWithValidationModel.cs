using System.Collections.Generic;
using System.Linq;

namespace Kanban.BusinessLogic.DTOs
{
    public class ResultWithValidationModel<T>
    {
        public readonly T Result;

        public readonly int StatusCode;

        public readonly IList<string> Errors;

        public bool IsSuccess => (Errors == null || !Errors.Any());

        public bool IsFailure => !IsSuccess;

        public static ResultWithValidationModel<T> Failure(int statusCode, params string[] errors)
        {
            return new ResultWithValidationModel<T>(statusCode, errors);
        }

        public static ResultWithValidationModel<T> Success(int statusCode, T result)
        {
            return new ResultWithValidationModel<T>(statusCode, result);
        }

        private ResultWithValidationModel()
        {
        }

        private ResultWithValidationModel(int statusCode, params string[] errors)
        {
            StatusCode = statusCode;
            Errors = errors.ToList();
        }

        private ResultWithValidationModel(int statusCode, T result)
        {
            StatusCode = statusCode;
            Result = result;
        }
        
    }
}
