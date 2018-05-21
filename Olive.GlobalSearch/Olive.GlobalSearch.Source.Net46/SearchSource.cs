using System;
using System.Security.Principal;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using System.Web;

namespace Olive.GlobalSearch
{
    partial class SearchSource : IHttpHandler
    {
        static string MakeAbsolute(string url) => HttpContext.Current.Request.GetAbsoluteUrl(url);

        bool IHttpHandler.IsReusable => false;

        void IHttpHandler.ProcessRequest(HttpContext context)
        {
            Keywords = context.Request["searcher"].OrEmpty().Split(' ');
            if (Keywords.None()) return;

            Process(context.User).GetAwaiter().GetResult();
            var response = new JavaScriptSerializer().Serialize(Results);
            context.Response.ContentType = "text/json";
            context.Response.Write(response);
        }

        /// <summary>
        /// Performs the search for a specified user query.
        /// </summary>
        /// <param name="user">The current http user who initiated the search.</param>        
        public abstract Task Process(IPrincipal user);
    }
}