using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Tasktower.ProjectService.Dtos;

namespace Tasktower.ProjectService.Errors
{
    public class AppException : Exception
    {
        public IEnumerable<AppException> MultipleErrors { get; private set; } = null;

        public HttpStatusCode StatusCode { get; private set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ErrorCode ErrorCode { get; private set; }

        public AppException(ErrorCode code, ErrorData errorData, IEnumerable<AppException> multipleErrors) 
            : base(errorData.Message)
        {
            ErrorCode = code;
            StatusCode = errorData.StatusCode;
            MultipleErrors = multipleErrors;
        }
    }
}
