using Olive.GlobalSearch.Common;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Olive.GlobalSearch
{
    public class SearchSource
    {
        public IEnumerable<T> GetQuery<T>(Func<T> func)
        {
            var database = Context.Current.Database();
        }
        public virtual SearchResult Process(ClaimsPrincipal user, string[] keywords)
        {
            if (user.IsInRole("Administrator"))
                return new SearchResult { Url = "Some url", Title = "Some title", Description = "Some description", IconUrl = "Some url" };
            else
                return new SearchResult { Url = "Some other url", Title = "Some other title", Description = "Some other description", IconUrl = "Some other url" };
        }
    }
}
