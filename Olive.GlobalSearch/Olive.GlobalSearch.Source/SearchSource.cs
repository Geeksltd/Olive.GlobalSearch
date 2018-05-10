namespace Olive.GlobalSearch
{
    using System.Collections.Generic;
    using System.Security.Claims;

    public abstract class SearchSource
    {
        public abstract IEnumerable<SearchResult> Process(ClaimsPrincipal user, string[] keywords);
    }
}