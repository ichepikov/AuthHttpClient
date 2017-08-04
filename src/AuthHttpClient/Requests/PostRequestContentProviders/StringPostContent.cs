using System.Net.Http;

namespace AuthHttpClient.Requests.PostRequestContentProviders
{
    public class StringPostContent : IPostRequestContent
    {
        private readonly string _data;

        public StringPostContent(string data)
        {
            _data = data;
        }

        public HttpContent GetContent()
        {
            return new StringContent(_data);
        }
    }
}
