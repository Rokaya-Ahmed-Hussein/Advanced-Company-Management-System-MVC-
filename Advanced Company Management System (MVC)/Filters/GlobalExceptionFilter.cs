using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Net;
using Advanced_Company_Management_System__MVC_.Models;

namespace Advanced_Company_Management_System__MVC_.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;
        private readonly IWebHostEnvironment _environment;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public void OnException(ExceptionContext context)
        {
            // Log the exception with full details
            _logger.LogError(context.Exception, 
                "An unhandled exception occurred. Path: {Path}, Method: {Method}, User: {User}",
                context.HttpContext.Request.Path,
                context.HttpContext.Request.Method,
                context.HttpContext.User?.Identity?.Name ?? "Anonymous");

            // Determine the status code
            var statusCode = context.Exception switch
            {
                ArgumentNullException => (int)HttpStatusCode.BadRequest,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };

            // Set the response status code
            context.HttpContext.Response.StatusCode = statusCode;

            // Check if this is an AJAX request
            var isAjaxRequest = context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            if (isAjaxRequest)
            {
                // Return JSON for AJAX requests
                context.Result = new JsonResult(new
                {
                    success = false,
                    message = _environment.IsDevelopment() 
                        ? context.Exception.Message 
                        : "An error occurred while processing your request.",
                    errorDetails = _environment.IsDevelopment() ? context.Exception.StackTrace : null
                })
                {
                    StatusCode = statusCode
                };
            }
            else
            {
                // Return error view for regular requests
                var modelMetadataProvider = context.HttpContext.RequestServices
                    .GetRequiredService<IModelMetadataProvider>();

                var result = new ViewResult
                {
                    ViewName = "Error",
                    ViewData = new ViewDataDictionary(modelMetadataProvider, context.ModelState)
                    {
                        Model = new ErrorViewModel
                        {
                            RequestId = System.Diagnostics.Activity.Current?.Id ?? context.HttpContext.TraceIdentifier,
                            StatusCode = statusCode,
                            ErrorMessage = _environment.IsDevelopment() 
                                ? context.Exception.Message 
                                : "An unexpected error occurred. Please try again later.",
                            ShowDetails = _environment.IsDevelopment(),
                            StackTrace = _environment.IsDevelopment() ? context.Exception.StackTrace : null
                        }
                    },
                    StatusCode = statusCode
                };

                context.Result = result;
            }

            context.ExceptionHandled = true;
        }
    }
}

