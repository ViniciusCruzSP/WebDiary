using DiaryApp.Models.Errors;

namespace DiaryApp.Exceptions
{
    public class ApiValidationException : Exception
    {
        public ApiValidationProblemDetailsDto ProblemDetails { get; }

        public ApiValidationException(ApiValidationProblemDetailsDto problemDetails)
        {
            ProblemDetails = problemDetails;
        }
    }
}
