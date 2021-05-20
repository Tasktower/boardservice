using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tasktower.ProjectService.Tools.Constants;

namespace Tasktower.ProjectService.Tools.Paging
{
    public class Pagination
    {
        [FromQuery(Name = "page")] public int Page { get; set; } = 0;
        [FromQuery(Name = "order_by")] public string OrderBy { get; set; } = "Id";
        
        [FromQuery(Name = "page_size")] public int PageSize { get; set; } = 20;
    }
}
