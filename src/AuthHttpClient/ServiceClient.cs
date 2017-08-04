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

        public async Task<RequestResponse<T>> Execute<T>(ServiceRequest<T> request)
        {
            try
            {
                request.Authentication = await _authenticationProvider.GetCredentials();
                request.BaseUrl = _baseUrl;
                return new RequestResponse<T>(await request.Execute());
            }
            catch (UnauthorizedAccessException)
            {
                return await Execute(request);
            }
            catch
            {
                return new RequestResponse<T>(default(T), RequestResponseErrorTypes.Unknown);
            }
        }
    }
}
