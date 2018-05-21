namespace Olive.GlobalSearch
{
    using Newtonsoft.Json;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using System.Security.Claims;

    public class GlobalSearchHandler<T> : DelegatingHandler where T : SearchSource, new()
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Note: TaskCompletionSource creates a task that does not contain a delegate.
            var tsc = new TaskCompletionSource<HttpResponseMessage>();

            var keywords = request.GetQueryNameValuePairs().Where(x => x.Key == "searcher");
            if (!keywords.Any())
            {
                var noResponse = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("")
                };
                tsc.SetResult(noResponse);
                return tsc.Task;
            }
            // Create the response.
            var searchInstance = new T();

            var result = searchInstance.Process(new ClaimsPrincipal(HttpContext.Current.User), keywords.Select(x => x.Value).FirstOrDefault().Split(' '));
            var responseObject = JsonConvert.SerializeObject(result);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {

                Content = new StringContent(responseObject)
            };


            tsc.SetResult(response);   // Also sets the task state to "RanToCompletion"
            return tsc.Task;
        }
    }
}
