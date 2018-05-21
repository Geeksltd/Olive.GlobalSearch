namespace Olive.GlobalSearch
{
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using System.Threading.Tasks;

    internal static class SearchApiMiddleware
    {
        internal static async Task Search<T>(HttpContext context) where T : SearchSource, new()
        {
            var keywords = context.Request.Param("searcher").OrEmpty().Split(' ');
            if (keywords.None()) return;

            var searchInstance = new T { Keywords = keywords };
            await searchInstance.Process(context.User);
            var response = JsonConvert.SerializeObject(searchInstance.Results);
            await context.Response.WriteAsync(response);
        }
    }
}
