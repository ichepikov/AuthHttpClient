using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AuthHttpClient.Authentication;
using AuthHttpClient.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AuthHttpClient.Requests
{
    public abstract class ServiceRequest<T>
    {
        private readonly string _resource;

        protected ServiceRequest(string resource)
        {
            _resource = resource;
        }

        internal Uri BaseUrl { get; set; }

        public Dictionary<string, string> QueryParameters { get; } = new Dictionary<string, string>();

        internal IAuthenticationCredentials Authentication { private get; set; }

        internal async Task<T> Execute()
        {
            HttpResponseMessage responseMessage;

            using (var client = CreateClient())
            {
                Authentication.Patch(client);
                responseMessage = await ExecuteIntenal(client);
            }

            using (responseMessage)
            {
                if (!responseMessage.IsSuccessStatusCode)
                {
                    if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        Authentication.Kill();
                        throw new UnauthorizedAccessException();
                    }
                    throw new Exception();
                }

                return await ReadParseResult(responseMessage);
            }
        }

        protected abstract Task<HttpResponseMessage> ExecuteIntenal(HttpClient client);

        protected virtual Uri BiuldUri()
        {
            var uriBuilder = new UriBuilder(new Uri(BaseUrl, _resource))
            {
                Port = -1,
                Query = HttpUtility.BuildQueryString(QueryParameters)
            };
            return uriBuilder.Uri;
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
            if (typeof(T) == typeof(string))
                return (T) (object) data;

            return JsonConvert.DeserializeObject<T>(data, new TimetacDateTimeJsonConverter());
        }
    }

    public class TimetacDateTimeJsonConverter : IsoDateTimeConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String && reader.Value.ToString() == "0000-00-00")
                return null;

            return base.ReadJson(reader, objectType, existingValue, serializer);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime?);
        }
    }
}
