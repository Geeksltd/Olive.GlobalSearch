namespace Olive.GlobalSearch
{
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using System.Threading.Tasks;

    internal static class SearchApiMiddleware
    {
        internal static async Task Search<T>(HttpContext context) where T : SearchSource, new()
        {
            var keywords = context.Request.Param("term").OrEmpty().Split(' ');
            if (keywords.None()) return;

            var searchInstance = new T();
            var result = searchInstance.Process(context.User, keywords);
            var response = JsonConvert.SerializeObject(result);
            await context.Response.WriteAsync(response);
        }
    }
}
