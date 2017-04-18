using System;
using System.Threading.Tasks;
using AuthHttpClient.Authentication;
using AuthHttpClient.Requests;
using AuthHttpClient.Responses;

namespace AuthHttpClient
{
    public class ServiceClient
    {
        private readonly IAuthenticationProvider _authenticationProvider;

        private readonly Uri _baseUrl;

        public ServiceClient(IAuthenticationProvider authenticationProvider, Uri baseUrl)
        {
            _authenticationProvider = authenticationProvider;
            _baseUrl = baseUrl;
        }

        public async Task<RequestResponce<T>> Execute<T>(ApiRequest<T> request)
        {
            try
            {
                request.Authentication = await _authenticationProvider.GetCredentials();
                request.BaseUrl = _baseUrl;
                return new RequestResponce<T>(await request.Execute());
            }
            catch (UnauthorizedAccessException)
            {
                request.Authentication.Kill();
                return await Execute(request);
            }
            catch
            {
                return new RequestResponce<T>(default(T), RequestErrorTypes.Unknown);
            }
        }
    }
}
