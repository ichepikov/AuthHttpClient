using System.Collections.Generic;
using System.Net.Http;

namespace AuthHttpClient.Requests.PostRequestContentProviders
{
    public class FormUrlEncodedPostContent : IPostRequestContent
    {
        public Dictionary<string, string> Content { get; } = new Dictionary<string, string>();

        public HttpContent GetContent()
        {
            return new FormUrlEncodedContent(Content);
        }
    }
}
