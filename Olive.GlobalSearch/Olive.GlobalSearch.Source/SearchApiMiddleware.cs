using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Olive.Web;

namespace Olive.GlobalSearch
{
    internal static class SearchApiMiddleware
    {
        internal static async Task Search<T>(HttpContext context) where T : SearchSource, new()
        {
            var term = context.Request.Query["term"];
            var user = Context.Current.User();
            var searchInstance = new T();
            var response = JsonConvert.SerializeObject(searchInstance.Process(user, term.ToString().Split(" ")));
            await context.Response.WriteAsync(response);
        }
    }
}
