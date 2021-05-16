using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Extensions.Configuration;
using Tasktower.ProjectService.Dtos;
using Tasktower.ProjectService.Errors;

namespace Tasktower.ProjectService.Configuration
{
    public class ErrorConfiguration
    {
        public Dictionary<string, ErrorData> ErrorCodeMappings { get; set; }
    }
}