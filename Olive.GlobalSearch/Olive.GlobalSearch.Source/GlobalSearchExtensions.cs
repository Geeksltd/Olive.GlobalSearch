namespace Olive
{
    using Microsoft.AspNetCore.Builder;
    using Olive.GlobalSearch;

    public static class GlobalSearchExtensions
    {
        public static IApplicationBuilder UseGlobalSearch<T>(this IApplicationBuilder @this)
            where T : SearchSource, new()
        {
            @this.Map("/api/global-search",
                app => app.Run(context => SearchApiMiddleware.Search<T>(context)));

            return @this;
        }
    }
}
