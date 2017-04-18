using System.Threading.Tasks;

namespace AuthHttpClient.Authentication.Implementation
{
    public class CommonAuthProvider : AuthProviderBase
    {
        private IAuthenticationRequest _authRequest;

        public async void SetAuthRequest(IAuthenticationRequest authRequest)
        {
            await CredantialsRequestLock.WaitAsync();
            _authRequest = authRequest;
            FatalError = false;
            CredantialsRequestLock.Release();
        }

        protected override async Task<IAuthenticationCredentials> RequestCredentials()
        {
            var localAuthrequest = _authRequest;
            if (localAuthrequest != null)
                return await localAuthrequest.ReAuthenticate();
            return null;
        }
    }
}
