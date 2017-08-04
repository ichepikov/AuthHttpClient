﻿using System.Net.Http;
using System.Threading.Tasks;
using AuthHttpClient.Requests.PostRequestContentProviders;

namespace AuthHttpClient.Requests
{
    public class PutServiceRequest<T> : ServiceRequest<T>
    {
        public IPostRequestContent Content { get; }

        public PutServiceRequest(string resource, IPostRequestContent content) : base(resource)
        {
            Content = content;
        }

        protected override async Task<HttpResponseMessage> ExecuteIntenal(HttpClient client)
        {
            using (var httpPostContent = Content.GetContent())
                return await client.PutAsync(BiuldUri(), httpPostContent);
        }
    }
}