using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Olive.GlobalSearch
{
    partial class SearchSource
    {
        static string MakeAbsolute(string url) => HttpContext.Current.Request.GetAbsoluteUrl(url);
    }
}
