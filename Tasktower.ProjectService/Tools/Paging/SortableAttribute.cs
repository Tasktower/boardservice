using System;

namespace Tasktower.ProjectService.Tools.Paging
{
    public class SortableAttribute : Attribute
    {
        public string OrderBy { get; set; }
    }
}