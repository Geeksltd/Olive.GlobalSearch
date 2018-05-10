using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Olive;
using System.Threading.Tasks;

namespace Olive.GlobalSearch
{
    public static class Search
    {
        public static async Task<IEnumerable<SearchResult>> GetResults(string keywords)
        {
            var results = new List<SearchResult>();
            var urls = Config.SettingsUnder("Olive.GlobalSearch:Sources").Select(x => x.Value);

            var parallel = await urls.Select(x => SearchSource(x, keywords)).AwaitAll();

            return parallel.SelectMany(x => x);
        }

        static Task<SearchResult[]> SearchSource(string url, string keywords)
        {
            return new ApiClient($"{url}api/search?term={keywords.UrlEncode()}")
                .AsHttpUser()
                .Get<SearchResult[]>();
        }

    }
}