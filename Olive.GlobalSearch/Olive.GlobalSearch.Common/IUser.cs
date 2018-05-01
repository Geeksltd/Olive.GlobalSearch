using System;
using System.Collections.Generic;
using System.Text;

namespace Olive.GlobalSearch.Common
{
    public interface IUser
    {
        bool IsInRole(string role);
    }
}
