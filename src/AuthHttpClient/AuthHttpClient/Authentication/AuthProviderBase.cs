using System;
using System.Threading;
using System.Threading.Tasks;

namespace AuthHttpClient.Authentication
{

    //ToDo: think what to do with fatal error.
    public abstract class AuthProviderBase : IAuthenticationProvider
    {
        protected readonly SemaphoreSlim CredantialsRequestLock = new SemaphoreSlim(1);
        private IAuthenticationCredentials _currentAuthenticationCredentials;

        public async Task<IAuthenticationCredentials> GetCredentials()
        {
            return await GetCredentialsWithRetry(1);
        }

        private async Task<IAuthenticationCredentials> GetCredentialsWithRetry(int retryCount)
        {
            for (int i = 0; i < retryCount + 1; i++)
            {
                if (FatalError)
                    throw new AuthException();

                var currentAuth = _currentAuthenticationCredentials;
                if (currentAuth?.IsAlive() == true)
                    return currentAuth;


                await CredantialsRequestLock.WaitAsync();
                try
                {
                    if (FatalError)
                        throw new AuthException();

                    if (_currentAuthenticationCredentials?.IsAlive() != true)
                    {
                        var result = await RequestCredentials();
                        if (result != null)
                            _currentAuthenticationCredentials = result;
                        else
                            FatalError = true;
                    }
                }
                finally
                {
                    CredantialsRequestLock.Release();
                }
            }

            throw new TimeoutException();
        }

        public bool FatalError { get; protected set; }

        protected abstract Task<IAuthenticationCredentials> RequestCredentials();
    }
}
