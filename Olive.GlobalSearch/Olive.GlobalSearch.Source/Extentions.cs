using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Olive.GlobalSearch
{
    public static class Extentions
    {
        public static IApplicationBuilder UseGlobalSearch<T>(this IApplicationBuilder @this) where T : SearchSource, new()
        {
            @this.Map("/api/search", app => { app.Run(async context => await SearchApiMiddleware.Search<T>(context)); });
            return @this;
        }
    }
}
