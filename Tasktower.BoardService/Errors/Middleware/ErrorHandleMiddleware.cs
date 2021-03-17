using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Tasktower.BoardService.Errors.Middleware
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

        private Task HandleException(HttpContext context, Exception ex)
        {
            string errorCode = null;
            string message;
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError; // 500 if unexpected
            IEnumerable<object> multipleErrors = null;

            // Specify different custom exceptions here
            if (ex is APIException webEx)
            {
                statusCode = webEx.StatusCode;
                errorCode = webEx.ErrorCode;
                message = webEx.Message;
                multipleErrors = webEx.MultipleErrors?
                    .Select(x => new { error = x.Message, code = x.ErrorCode });
            }
            else
            {
                message = _options.ShowAllErrorMessages ? ex.Message : "Internal server error";
            }
            string result = JsonSerializer.Serialize(new
            {
                error = message,
                stackTrace = _options.UseStackTrace ?
                    ex.StackTrace.Split(Environment.NewLine).Select(x => x.Trim()) : null,
                errorCode,
                multipleErrors,
                status = statusCode
            }, Tools.JsonTools.JsonSerializerUtils.CustomSerializerOptions()); ;

            if (_options.UseStackTrace)
            {
                _logger.LogError(
                    $"Exception: {ex.GetType().FullName ?? ex.GetType().Name}{Environment.NewLine}" +
                    $"Message: {ex.Message}{Environment.NewLine}" +
                    ((errorCode is not null) ? $"Error Code: {errorCode?.ToString() ?? null}{Environment.NewLine}" : "") +
                    $"Stacktrace: {Environment.NewLine}" +
                    $"{ex.StackTrace}");
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(result);
        }
    }
}
