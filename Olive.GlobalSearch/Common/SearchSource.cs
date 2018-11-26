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
        internal EntityListResult<SearchResult> Results = new EntityListResult<SearchResult>();

        /// <summary>
        /// A set of keywords specified by the user.
        /// This will never be null or empty.
        /// Each entry will be trimmed and have a value.
        /// </summary>
        public string[] Keywords { get; internal set; }



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
        /// If you Use this method to add items, Pagination arguments will be set automatically
        /// Note: Do not use Set method if you use Add method.
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

            var list = Results.Data as List<SearchResult>;
            if (list == null) list = new List<SearchResult>();
            list.Add(result);
            Results.Data = list;
            Results.TotalCount++;
            Results.Size = (int)Results.TotalCount;
        }

        static string FixUrl(string url)
        {
            if (url.OrEmpty().StartsWith("~/")) return MakeAbsolute(url.TrimStart('~'));
            else return url;
        }

        /// <summary>
        /// Adds an item to the results.
        /// Note: Do not use Set method if you use Add method.
        /// <paramref name="url">For relative Url to the current site use ~/my-url syntax.</paramref>
        /// </summary>
        protected void Add(string title, string url)
        {
            Add(new SearchResult { Title = title, Url = url });
        }

        /// <summary>
        /// Set Results, You can use this method to set data all in once
        /// Note: Do not use Add method if you use Set method.
        /// </summary>
        /// <param name="result"></param>
        protected void Set(EntityListResult<SearchResult> result)
        {
            if (result == null)
                throw new ArgumentException("Null value can not set to result.");
            if (result.Data == null)
                throw new ArgumentException("Data Cannot be null, it should be an instance of IEnumerable object.");
            if (result.TotalCount < 0)
                throw new ArgumentException("TotalCount can not be lower that 0.");
            if (result.Size < 0)
                throw new ArgumentException("Size can not be lower that 0.");
            if (result.StartIndex < 0)
                throw new ArgumentException("StartIndex can not be lower that 0.");
            if (result.StartIndex > result.TotalCount)
                throw new ArgumentException("StartIndex can not be greater that TotalCount.");

            Results = result;
        }

        /// <summary>
        /// Set Results, You can use this method to set data all in once
        /// Note: Do not use Add method if you use Set method.
        /// </summary>
        /// <param name="Data">A collection of SearchResult such as list,array or other IEnumerable of SearchResult.</param>
        /// <param name="TotalCount">Total count of result</param>
        /// <param name="StartIndex">Starting Index of Data, using in pagination arguments</param>
        /// <param name="Size">The size of items per page, using in pagination arguments</param>
        protected void Set(IEnumerable<SearchResult> Data, long TotalCount, int StartIndex, int Size)
        {
            Set(new EntityListResult<SearchResult>() { Data = Data, TotalCount = TotalCount, StartIndex = StartIndex, Size = Size });
        }
    }
}