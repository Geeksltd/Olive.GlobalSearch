using Olive.GlobalSearch.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Olive.GlobalSearch.UI
{
    public static class Search
    {
        public static async Task<IEnumerable<SearchResult>> AutoComplete(string keywords)
        {
            var results = new List<SearchResult>();
            var urls = Config.Get("Me:Name");
            System.Diagnostics.Debug.WriteLine(urls);
            results.AddRange(await new ApiClient($"http://localhost:9015/api/search?term={keywords}").AsHttpUser().Get<SearchResult[]>());
            //TODO: Get from settings
            //foreach (var url in urls)
            // results.AddRange(await new ApiClient($"{url.Value}/api/search?term={keywords}").AsHttpUser().Get<SearchResult[]>());

            return results;
        }
    }
}
