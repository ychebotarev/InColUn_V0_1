using System;
using System.Net.Http;

namespace InColUn.Auth
{
    public class OAuthServiceOptions
    {
        public TimeSpan BackchannelTimeout { get; set; } = TimeSpan.FromSeconds(60);
        public HttpMessageHandler BackchannelHttpHandler { get; set; }
    }
}
