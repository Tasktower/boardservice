using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasktower.BoardService.Security
{
    public static class Scopes
    {
        public const string READ_BOARD = "read:board";
        public const string WRITE_BOARD = "write:board";
    }
}
