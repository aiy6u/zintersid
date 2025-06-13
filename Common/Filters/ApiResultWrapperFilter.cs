using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using zintersid.Common.CustomAttributes;
using zintersid.Common.Models;

namespace zintersid.Common.Filters
{
    public class ApiResultWrapperFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var endpoint = context.HttpContext.GetEndpoint();
            var dontWrap = endpoint?.Metadata.GetMetadata<DontWrapAttribute>() != null;

            if (!dontWrap)
            {
                if (context.Result is ObjectResult objectResult)
                {
                    // Avoid double-wrapping
                    if (objectResult.Value is not ApiResult<object>)
                    {
                        bool isError = objectResult.StatusCode >= 400;
                        if (isError)
                        {
                            string? message = objectResult.Value switch
                            {
                                ProblemDetails pd => pd.Title ?? pd.Detail ?? "An error occurred.",
                                string s => s,
                                _ => "An error occurred."
                            };
                            var wrapped = ApiResult<object>.ErrorResult(message ?? "An error occurred.");
                            wrapped.Data = objectResult.Value;
                            context.Result = new ObjectResult(wrapped)
                            {
                                StatusCode = objectResult.StatusCode
                            };
                        }
                        else
                        {
                            var wrapped = ApiResult<object>.SuccessResult(objectResult.Value ?? new object());
                            context.Result = new ObjectResult(wrapped)
                            {
                                StatusCode = objectResult.StatusCode
                            };
                        }
                    }
                }
                else if (context.Result is EmptyResult)
                {
                    context.Result = new ObjectResult(ApiResult<object>.SuccessResult(new object()));
                }
                else if (context.Result is StatusCodeResult statusCodeResult)
                {
                    bool isError = statusCodeResult.StatusCode >= 400;
                    if (isError)
                    {
                        context.Result = new ObjectResult(ApiResult<object>.ErrorResult($"Status code: {statusCodeResult.StatusCode}"))
                        {
                            StatusCode = statusCodeResult.StatusCode
                        };
                    }
                    else
                    {
                        context.Result = new ObjectResult(ApiResult<object>.SuccessResult(new object()))
                        {
                            StatusCode = statusCodeResult.StatusCode
                        };
                    }
                }
            }

            await next();
        }
    }
}