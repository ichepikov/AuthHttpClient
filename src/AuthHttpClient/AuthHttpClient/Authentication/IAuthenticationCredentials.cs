using System.Net.Http;

namespace AuthHttpClient.Authentication
{
    public interface IAuthenticationCredentials
    {
        void Patch(HttpClient client);
        bool IsAlive();
        void Kill();
    }
}
