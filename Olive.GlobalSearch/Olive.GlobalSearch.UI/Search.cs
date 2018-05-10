using Olive.GlobalSearch.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Olive.GlobalSearch.UI
{
    public static class Search
    {
        public static async Task<IEnumerable<SearchResult>> GetResults(string keywords)
        {
            var results = new List<SearchResult>();
            var urls = Config.SettingsUnder("Olive.GlobalSearch:Sources");
            foreach (var url in urls)
                results.AddRange(await new ApiClient($"{url.Value}api/search?term={keywords}").AsHttpUser().Get<SearchResult[]>());

            return results;
        }
    }
}
