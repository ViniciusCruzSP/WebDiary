using DiaryApp.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DiaryApp.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ApiValidationException validationEx)
            {
                foreach (var error in validationEx.ProblemDetails.Errors)
                {
                    foreach (var message in error.Value)
                    {
                        context.ModelState.AddModelError(error.Key, message);
                    }
                }

                context.Result = new ViewResult
                {
                    ViewData =
                    new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary(
                        new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(),context.ModelState
                    )
                };

                context.ExceptionHandled = true;
                return;
            }
            if (context.Exception is ApiNotFoundException)
            {
                context.Result = new RedirectToActionResult(
                    "Diary",
                    context.RouteData.Values["controller"]?.ToString(),
                    null
                );

                context.ExceptionHandled = true;
                return;
            }
        }
    }
}
