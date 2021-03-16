using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasktower.BoardService.Helpers.Paging
{
    public class Page<T> where T : class
    {
        public IList<T> Items { get; set; }
        public int Count { get; set; }

        public Pagination Pagination { get; set; }
    }
}
