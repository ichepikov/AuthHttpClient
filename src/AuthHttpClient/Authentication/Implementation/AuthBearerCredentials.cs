using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace AuthHttpClient.Authentication.Implementation
{
    public class AuthBearerCredentials : IAuthenticationCredentials
    {
        private readonly DateTime _expiteDateTime;
        private readonly string _accessToken;
        private bool _isAlive = true;

        public AuthBearerCredentials(string accessToken) : this(accessToken, DateTime.MaxValue)
        {

        }

        public AuthBearerCredentials(string accessToken, DateTime liveTimeSpan)
        {
            _accessToken = accessToken;
            _expiteDateTime = liveTimeSpan;
        }

        public void Patch(HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                @"Bearer", _accessToken);
        }

        public bool IsAlive()
        {
            if (!_isAlive)
                return false;

            if (_expiteDateTime < DateTime.Now)
                Kill();

            return _isAlive;
        }

        public void Kill()
        {
            _isAlive = false;
        }
    }
}
