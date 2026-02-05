using Microsoft.AspNetCore.Mvc;

namespace WebDiaryAPI.Models.Errors
{
    public class ApiValidationProblemDetails : ProblemDetails
    {
        public IDictionary<string, string[]> Errors { get; }

        public ApiValidationProblemDetails(IDictionary<string, string[]> errors)
        {
            Title = "Validation error";
            Status = StatusCodes.Status400BadRequest;
            Detail = "One or more validation errors occurred;";
            Type = "https://httpstatuses.com/400";
            Errors = errors;
        }
    }
}
