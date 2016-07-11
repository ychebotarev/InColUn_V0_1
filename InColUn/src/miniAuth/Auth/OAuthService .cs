using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace InColUn.Auth
{
    public class OAuthService
    {
        Dictionary<string, IOAuthStrategy> strategies;
        protected HttpClient Backchannel { get; private set; }

        public OAuthService(OAuthServiceOptions options)
        {
            this.Backchannel = new HttpClient(options.BackchannelHttpHandler ?? new HttpClientHandler());
            this.Backchannel.DefaultRequestHeaders.UserAgent.ParseAdd("InColUn middleware");
            this.Backchannel.Timeout = options.BackchannelTimeout;
            this.Backchannel.MaxResponseContentBufferSize = 1024 * 1024 * 10; // 10 MB

            this.strategies = new Dictionary<string, IOAuthStrategy>();
        }

        public async Task StartAuthentificationAsync(HttpContext context, string authScheme)
        {
            var strategy = this[authScheme];
            await strategy.StartOAuthAsync(context);
        }

        public void AddStrategy(IOAuthStrategy strategy)
        {
            this.strategies[strategy.AuthScheme] = strategy;
            strategy.SetBackChannel(this.Backchannel);
        }

        public IOAuthStrategy this[string authScheme]
        {
            get
            {
                return this.strategies[authScheme];
            }
            set
            {
                value.SetBackChannel(this.Backchannel);
                this.strategies[authScheme] = value;
            }
        }
    }
}