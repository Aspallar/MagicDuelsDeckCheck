using System;
using System.Net;

namespace MagicDuelsDeckCheck
{
    internal class UserAgentWebClient : WebClient
    {
        private string _userAgent;

        public UserAgentWebClient(string userAgent) : base()
        {
            _userAgent = userAgent;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = (HttpWebRequest)base.GetWebRequest(address);
            request.UserAgent = _userAgent;
            return request;
        }
    }
}
