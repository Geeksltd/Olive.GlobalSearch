namespace Olive.GlobalSearch
{
    using System.Collections.Generic;
    using System.Security.Claims;

    /// <summary>
    /// The base class for a custom application-specific source provider.
    /// </summary>
    public abstract class SearchSource
    {
        /// <summary>
        /// Performs the search for a specified user query.
        /// </summary>
        /// <param name="user">The current http user who initiated the search.</param>
        /// <param name="keywords">A list of keywords typed in by the user.</param>
        public abstract IEnumerable<SearchResult> Process(ClaimsPrincipal user, string[] keywords);
    }
}