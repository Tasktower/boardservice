using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tasktower.ProjectService.Errors
{
    public class ApiException : Exception
    {
        public enum Code
        {
            INTERNAL_SERVER_ERROR,
            ACCOUNT_NOT_FOUND,
            ACCOUNT_NOT_REGISTERED_STANDARD,
            ACCOUNT_FAILED_TO_CREATE,
            ACCOUNT_ALREADY_EXISTS,
            MULTIPLE_EXCEPTIONS_FOUND,
            UNAUTHORIZED,
            FORBIDDEN
        }

        public static ApiException CreateFromMultiple(IEnumerable<ApiException> multipleExceptions)
        {
            var apiEx = Create(Code.MULTIPLE_EXCEPTIONS_FOUND);
            apiEx.MultipleErrors = multipleExceptions;
            return apiEx;
        }

        public static ApiException Create(Code code, params string[] args)
        {
            return code switch
            {
                Code.ACCOUNT_ALREADY_EXISTS => new ApiException(code, HttpStatusCode.BadRequest, "Account already exists", args),
                Code.ACCOUNT_NOT_FOUND => new ApiException(code, HttpStatusCode.BadRequest, "Account not found", args),
                Code.ACCOUNT_FAILED_TO_CREATE => new ApiException(code, HttpStatusCode.BadRequest, "Account failed to create", args),
                Code.MULTIPLE_EXCEPTIONS_FOUND => new ApiException(code, HttpStatusCode.BadRequest, "Multiple exceptions found", args),
                Code.UNAUTHORIZED => new ApiException(code, HttpStatusCode.Unauthorized, "Unauthorized", args),
                Code.FORBIDDEN => new ApiException(code, HttpStatusCode.Forbidden, "Forbidden", args),
                Code.INTERNAL_SERVER_ERROR => new ApiException(code, HttpStatusCode.InternalServerError, "Something went wrong", args),
                _ => new ApiException(Code.INTERNAL_SERVER_ERROR, HttpStatusCode.InternalServerError, "Something went wrong", args),
            };
        }

        public IEnumerable<ApiException> MultipleErrors { get; protected set; } = null;

        public HttpStatusCode StatusCode { get; private set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public string ErrorCode { get; private set; }

        private ApiException(Code code, HttpStatusCode statusCode, string messageFormat, params string[] args)
            : base(string.Format(messageFormat, args))
        {
            ErrorCode = code.ToString();
            StatusCode = statusCode;
        }
    }
}
