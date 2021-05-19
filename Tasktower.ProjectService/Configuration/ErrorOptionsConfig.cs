using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Extensions.Configuration;
using Tasktower.ProjectService.Dtos;
using Tasktower.ProjectService.Errors;

namespace Tasktower.ProjectService.Configuration
{
    public class ErrorOptionsConfig
    {
        public Dictionary<string, ErrorConfigData> ErrorCodeMappings { get; set; }
        public bool UseStackTrace { get; set; }
        public bool ShowAllErrorMessages { get; set; }
        public class ErrorConfigData
        {
            public HttpStatusCode StatusCode { get; set; }
            public string Message { get; set; }
        }
    }
}