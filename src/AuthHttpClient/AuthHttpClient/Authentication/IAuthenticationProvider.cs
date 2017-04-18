using System.Threading.Tasks;

namespace AuthHttpClient.Authentication
{
    public interface IAuthenticationProvider
    {
        Task<IAuthenticationCredentials> GetCredentials();
    }
}