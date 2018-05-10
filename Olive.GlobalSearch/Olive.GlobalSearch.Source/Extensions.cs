using Microsoft.AspNetCore.Builder;

namespace Olive.GlobalSearch
{
    public static class Extensions
    {
        public static IApplicationBuilder UseGlobalSearch<T>(this IApplicationBuilder @this) where T : SearchSource, new()
        {
            @this.Map("/api/search", app => { app.Run(async context => await SearchApiMiddleware.Search<T>(context)); });
            return @this;
        }
    }
}
