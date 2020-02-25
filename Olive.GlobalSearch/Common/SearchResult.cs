namespace Olive.GlobalSearch
{
    /// <summary>
    /// Represents a single item that is displayed to the user.
    /// </summary>
    public class SearchResult
    {
        /// <summary>
        /// Url to which the user will be redirected. This is mandatory.
        /// For relative Url to the current site use ~/my-url syntax.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Title of the search result. This is mandatory.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Optional description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// For relative Url to the current site use ~/my-url syntax.
        /// </summary>
        public string IconUrl { get; set; }

        /// <summary>
        /// This colour code will be used for search result. ex: #FFFFFF
        /// </summary>
        public string Colour { get; set; }
    }
}
