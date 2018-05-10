using Olive.GlobalSearch;
using Olive.GlobalSearch.UI.Properties;
using Scriban;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Olive
{
    public static class GlobalSearchExtensions
    {
        public static IEnumerable<JsonItem> AutoComplete(this IEnumerable<SearchResult> @this)
        {
            foreach (var item in @this)
            {
                yield return new JsonItem
                {
                    Display = item.ToHtml(),
                    Text = item.Title,
                    Value = item.Url
                };
            }
        }
        private static string ToHtml(this SearchResult @this)
        {
            var template = Template.ParseLiquid(Resources.Template);
            return template.Render(@this);
        }
    }
}