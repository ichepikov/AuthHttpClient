using System;
using System.Threading.Tasks;
using AuthHttpClient.Authentication;

namespace AuthHttpClient.Requests
{
    public abstract class ApiRequestBase<T>
    {
        internal IAuthenticationCredentials Authentication { get; set; }

        internal Uri BaseUrl { get; set; }

        internal abstract Task<T> Execute();
    }
}
