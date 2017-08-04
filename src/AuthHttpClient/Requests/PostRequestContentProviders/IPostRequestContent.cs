using System.Net.Http;

namespace AuthHttpClient.Requests.PostRequestContentProviders
{
    public interface IPostRequestContent
    {
        HttpContent GetContent();
    }
}
