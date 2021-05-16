using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Tasktower.ProjectService.Errors.Middleware
{
    public class ErrorHandeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ErrorHandleMiddlewareOptions _options;
        private readonly ILogger _logger;

        public ErrorHandeMiddleware(RequestDelegate next,
            ErrorHandleMiddlewareOptions options,
            ILogger<ErrorHandeMiddleware> logger)
        {
            _next = next;
            _options = options;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private Task HandleException(HttpContext context, Exception exception)
        {
            string errorCode = null;
            string message;
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError; // 500 if unexpected
            IEnumerable<object> multipleErrors = null;

            // Specify different custom exceptions here
            if (exception is AppException appException)
            {
                statusCode = appException.StatusCode;
                errorCode = appException.ErrorCode.ToString();
                message = appException.Message;
                multipleErrors = appException.MultipleErrors?
                    .Select(x => new { error = x.Message, code = x.ErrorCode.ToString() });
            }
            else
            {
                message = _options.ShowAllErrorMessages ? exception.Message : "Internal server error";
            }
            string result = JsonSerializer.Serialize(new
            {
                error = message,
                errorCode,
                multipleErrors,
                status = statusCode
            }, Tools.JsonTools.JsonSerializerUtils.CustomSerializerOptions()); ;

            if (_options.UseStackTrace)
            {
                _logger.LogTrace("Exception: {0}{1}Message: {2}{3}Error Code: {4}{5}Stacktrace: {6}{7}",
                    exception.GetType().FullName ?? exception.GetType().Name, Environment.NewLine,
                    exception.Message, Environment.NewLine,
                    errorCode ?? "", Environment.NewLine, 
                    Environment.NewLine, 
                    exception.StackTrace ?? "");
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(result);
        }
    }
}
