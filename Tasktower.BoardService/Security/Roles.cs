﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tasktower.BoardService.Security
{
    public static class Roles
    {
        public const string ADMIN = "ADMIN";
        public const string MODERATOR = "MODERATOR";
    }
}