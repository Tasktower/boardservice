using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasktower.ProjectService.Tools.Paging
{
    public class Page<T>
    {
        public IEnumerable<T> ResultsList { get; set; }
        public int ResultsSize { get; set; }
        public int Total { get; set; }
        public Pagination Pagination { get; set; }
        
        public Page<TNew> Map<TNew>(Func<T, TNew> func)
        {
            return new()
            {
                ResultsList = ResultsList.Select(func),
                ResultsSize = ResultsSize,
                Total = Total,
                Pagination = Pagination
            };
        }
    }
}
