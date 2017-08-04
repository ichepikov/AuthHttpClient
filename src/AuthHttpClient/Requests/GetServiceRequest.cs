using System.Net.Http;
using System.Threading.Tasks;

namespace AuthHttpClient.Requests
{
    public class GetServiceRequest<T> : ServiceRequest<T>
    {
        public GetServiceRequest(string resource) : base(resource)
        {
        }

        protected override async Task<HttpResponseMessage> ExecuteIntenal(HttpClient client)
        {
            return await client.GetAsync(BiuldUri());
        }
    }
}
