using System;
using System.Collections.Generic;
using System.Text;

namespace Olive.GlobalSearch
{
    partial class SearchSource
    {
        static string MakeAbsolute(string url) => Context.Current.Request().GetAbsoluteUrl(url);
    }
}
