using System.Net.Http;
using System.Threading.Tasks;

namespace AuthHttpClient.Requests
{
    public class DeleteServiceRequest<T> : ServiceRequest<T>
    {
        public DeleteServiceRequest(string resource) : base(resource)
        {
        }

        protected override async Task<HttpResponseMessage> ExecuteIntenal(HttpClient client)
        {
            return await client.DeleteAsync(BiuldUri());
        }
    }
}
