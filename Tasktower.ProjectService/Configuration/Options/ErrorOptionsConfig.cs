using System.Collections.Generic;
using System.Net;

namespace Tasktower.ProjectService.Configuration.Options
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