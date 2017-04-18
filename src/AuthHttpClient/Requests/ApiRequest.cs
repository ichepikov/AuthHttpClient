using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AuthHttpClient.Authentication;
using Newtonsoft.Json;

namespace AuthHttpClient.Requests
{
    //ToDo: HttpMethod? 
    //ToDo: Parcer?
    public class ApiRequest<T>
    {
        private readonly string _resource;

        public ApiRequest(string resource)
        {
            _resource = resource;
        }

        internal Uri BaseUrl { get; set; }

        public HttpMethod Method { get; set; }

        internal IAuthenticationCredentials Authentication { get; set; }

        internal async Task<T> Execute()
        {
            HttpResponseMessage responseMessage;

            using (var client = CreateClient())
            {
                Authentication.Patch(client);
                responseMessage = await ExecuteIntenal(client);
            }

            T result;

            using (responseMessage)
            {
                if (!responseMessage.IsSuccessStatusCode)
                {
                    if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                        throw new UnauthorizedAccessException();
                    else
                    {

                    }
                }

                result = await ReadParseResult(responseMessage);
            }

            return result;
        }

        protected virtual Task<HttpResponseMessage> ExecuteIntenal(HttpClient client)
        {
            return client.GetAsync(BiuldUri());
        }

        protected virtual Uri BiuldUri()
        {
            return new Uri(BaseUrl, _resource);
        }

        protected virtual HttpClient CreateClient()
        {
            return new HttpClient();
        }

        protected virtual async Task<T> ReadParseResult(HttpResponseMessage httpResponseMessage)
        {
            var stringContent = await httpResponseMessage.Content.ReadAsStringAsync();
            return ParseStringResult(stringContent);
        }

        protected virtual T ParseStringResult(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
