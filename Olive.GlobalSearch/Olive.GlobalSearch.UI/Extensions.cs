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
                    Display = item.Title,
                    Text = item.Title,
                    Value = item.Url
                };
            }
        }

        public static IEnumerable<JsonItem> RenderHtml(this IEnumerable<SearchResult> @this, string templatePath)
        {
            foreach (var item in @this)
            {
                yield return new JsonItem
                {
                    Display = item.ToHtml(templatePath),
                    Text = item.Title,
                    Value = item.Url
                };
            }
        }

        public static IEnumerable<JsonItem> RenderHtml(this IEnumerable<SearchResult> @this) => @this.RenderHtml(Resources.Template);
        private static string ToHtml(this SearchResult @this, string templatePath)
        {
            var template = Template.ParseLiquid(templatePath);
            return template.Render(@this);
        }
    }
}