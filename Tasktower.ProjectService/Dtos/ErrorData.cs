using System;
using System.Net;
using Tasktower.ProjectService.Errors;

namespace Tasktower.ProjectService.Dtos
{
    public class ErrorData
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }
}