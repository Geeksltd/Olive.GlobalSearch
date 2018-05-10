using Olive.GlobalSearch.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Olive.GlobalSearch.UI
{
    public static class Extensions
    {
        public static IEnumerable<JsonItem> ToJsonItem(this IEnumerable<SearchResult> @this)
        {
            foreach (var item in @this)
            {
                yield return new JsonItem
                {
                    Display = item.Title,
                    Text = item.Title,
                    Value = item.Url
                };
            }
        }
    }
}
