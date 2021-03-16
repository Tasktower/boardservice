using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasktower.BoardService.Errors.Middleware
{
    public class ErrorHandleMiddlewareOptions
    {
        public bool UseStackTrace { get; set; }
        public bool ShowAllErrorMessages { get; set; }
    }
}
