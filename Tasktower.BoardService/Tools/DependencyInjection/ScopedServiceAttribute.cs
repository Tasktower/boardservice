using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasktower.BoardService.Tools.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ScopedServiceAttribute : Attribute
    {
    }
}
