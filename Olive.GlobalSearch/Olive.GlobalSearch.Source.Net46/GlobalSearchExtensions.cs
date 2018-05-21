namespace Olive
{
    using Olive.GlobalSearch;
    using System.Web.Http;

    public static class GlobalSearchExtensions
    {
        public static void UseGlobalSearch<T>(this HttpConfiguration @this)
            where T : SearchSource, new()
        {
            @this.Routes.MapHttpRoute(
               name: "searchMap",
               routeTemplate: "api/search",
               defaults: null,
               constraints: null,
               handler: new GlobalSearchHandler<T>()
               );
        }
    }
}