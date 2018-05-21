using System.Security.Claims;
using System.Threading.Tasks;

namespace Olive.GlobalSearch
{
    partial class SearchSource
    {
        static string MakeAbsolute(string url) => Context.Current.Request().GetAbsoluteUrl(url);

        /// <summary>
        /// Performs the search for a specified user query.
        /// </summary>
        /// <param name="user">The current http user who initiated the search.</param>        
        public abstract Task Process(ClaimsPrincipal user);
    }
}
