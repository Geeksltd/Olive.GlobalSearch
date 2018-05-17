using Olive.GlobalSearch;
using Olive.GlobalSearch.UI.Properties;
using Scriban;
using System.Collections.Generic;

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
                    Value = item
                };
            }
        }

        public static IEnumerable<JsonItem> RenderHtml(this IEnumerable<SearchResult> @this, string templateString)
        {
            foreach (var item in @this)
            {
                yield return new JsonItem
                {
                    Display = item.ToHtml(templateString),
                    Text = item.Title,
                    Value = item
                };
            }
        }

        public static IEnumerable<JsonItem> RenderHtml(this IEnumerable<SearchResult> @this) => @this.RenderHtml(Resources.Template);

        static string ToHtml(this SearchResult @this, string templateString)
        {
            var template = Template.ParseLiquid(templateString);
            return template.Render(@this);
        }
    }
}