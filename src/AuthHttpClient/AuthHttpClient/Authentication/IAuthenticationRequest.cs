using System.Threading.Tasks;

namespace AuthHttpClient.Authentication
{
    public interface IAuthenticationRequest
    {
        Task<IAuthenticationCredentials> ReAuthenticate();
    }
}