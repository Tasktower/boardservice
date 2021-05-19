using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Tasktower.ProjectService.Configuration;
using Tasktower.ProjectService.Dtos;

namespace Tasktower.ProjectService.Errors
{
    public class AppException : Exception
    {
        public IEnumerable<AppException> MultipleErrors { get; }

        public HttpStatusCode StatusCode { get; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ErrorCode ErrorCode { get; }

        public AppException(ErrorCode code, ErrorOptionsConfig.ErrorConfigData errorConfigData, 
            IEnumerable<AppException> multipleErrors) 
            : base(errorConfigData.Message)
        {
            ErrorCode = code;
            StatusCode = errorConfigData.StatusCode;
            MultipleErrors = multipleErrors;
        }
    }
}
