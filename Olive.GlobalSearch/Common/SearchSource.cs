namespace Olive.GlobalSearch
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Olive;
    using System;
    using System.Linq;

    /// <summary>
    /// The base class for a custom application-specific source provider.
    /// </summary>
    public abstract partial class SearchSource
    {
        internal List<SearchResult> Results = new List<SearchResult>();

        /// <summary>
        /// A set of keywords specified by the user.
        /// This will never be null or empty.
        /// Each entry will be trimmed and have a value.
        /// </summary>
        public string[] Keywords { get; internal set; }

        /// <summary>
        /// Performs the search for a specified user query.
        /// </summary>
        /// <param name="user">The current http user who initiated the search.</param>        
        public abstract Task Process(ClaimsPrincipal user);

        /// <summary>
        /// Determines whether the specified object's ToString() value or any of its specified properties together will match all the specified keywords of the search.
        /// </summary>
        protected bool MatchesKeywords<T>(T item, params Func<T, string>[] properties) where T : class
        {
            if (item == null) return false;

            return properties.Select(x => x(item)).Concat(item.ToStringOrEmpty())
                .ToString(" ").ContainsAll(Keywords, caseSensitive: false);
        }

        /// <summary>
        /// Adds an item to the results.
        /// </summary>
        protected void Add(SearchResult result)
        {
            if (result == null) return;

            if (result.Url.IsEmpty())
                throw new ArgumentException("Url cannot be empty in a search result.");

            if (result.Title.IsEmpty())
                throw new ArgumentException("Title cannot be empty in a search result.");

            result.Url = FixUrl(result.Url);
            result.IconUrl = FixUrl(result.IconUrl);
            Results.Add(result);
        }

        static string FixUrl(string url)
        {
            if (url.OrEmpty().StartsWith("~/")) return MakeAbsolute(url.TrimStart('~'));
            else return url;
        }

        /// <summary>
        /// Adds an item to the results.
        /// <paramref name="url">For relative Url to the current site use ~/my-url syntax.</paramref>
        /// </summary>
        protected void Add(string title, string url)
        {
            Add(new SearchResult { Title = title, Url = url });
        }
    }
}