using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Olive.GlobalSearch.Common
{
    class User : IUser
    {
        public bool IsInRole(string role) => Role.Contains(role);
        public string[] Role { get; set; }
    }
}
