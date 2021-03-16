using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasktower.BoardService.Helpers.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ScopedServiceAttribute : Attribute
    {
    }
}
