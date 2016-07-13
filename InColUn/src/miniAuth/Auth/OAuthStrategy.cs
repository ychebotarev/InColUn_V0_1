using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InColUn.Auth
{
    public abstract class OAuthStrategy<TOptions>: IOAuthStrategy where TOptions : OAuthStrategyOptions, new()
    {
        public abstract string AuthScheme { get; }

        private const string StateSecret = "InColUn";

        protected byte[] uniqueKey;
        protected HttpClient Backchannel { get; private set; }
        protected TOptions Options { get; private set; }

        public OAuthStrategy(TOptions options)
        {
            this.Options = options;
            this.uniqueKey = Encryption.AESThenHMAC.NewKey();
            this.Validate();
        }

        public OAuthStrategyOptions GetOptions()
        {
            return this.Options;
        }

        public void SetBackChannel(HttpClient client)
        {
            this.Backchannel = client;
        }

        public Task StartOAuthAsync(HttpContext context)
        {
            this.StartOAuth(context);
            return Task.FromResult(0);
        }

        public async Task ProcessCallbackAsync(HttpContext context)
        {
            var query = context.Request.Query;

            var error = query["error"];
            if (!StringValues.IsNullOrEmpty(error))
            {
                await this.ProcessError(context);
                return;
            }

            var code = query["code"];
            var state = query["state"];

            if (!this.CheckOAuthState(state))
            {
                await this.ProcessStateError(context);
                return;
            }
            var token = await this.GetTokenAsync(context, code);
            if(string.IsNullOrEmpty(token.AccessToken))
            {
                await this.ProcessCustomError("access token is missing");
                return;
            }

            var claims = await this.GetUserClaimsAsync(context, token);

            if(this.Options.OnOAuthSuccess != null)
                await this.Options.OnOAuthSuccess(claims, this.AuthScheme);
        }


        public void StartOAuth(HttpContext context)
        {
            var challengeUrl = this.BuildChallengeUrl(context);

            context.Response.Redirect(challengeUrl);
        }

        protected abstract Task<ClaimsIdentity> GetUserClaimsAsync(HttpContext context, OAuthTokenResponse token);

        protected virtual string BuildChallengeUrl(HttpContext context)
        {
            var scope = this.Options.FormatScope();
            var redirectUri = this.BuildRedirectUri(context);

            var queryBuilder = new QueryBuilder()
            {
                { "client_id", this.Options.ClientId },
                { "scope", scope },
                { "response_type", "code" },
                { "redirect_uri", redirectUri },
                { "state", this.GenerateState() },
            };

            return this.Options.AuthorizationEndpoint + queryBuilder.ToString();
        }

        protected virtual async Task<OAuthTokenResponse> GetTokenAsync(HttpContext context, string code)
        {
            var redirectUri = this.BuildRedirectUri(context);

            var tokenRequestParameters = new Dictionary<string, string>()
            {
                { "client_id", this.Options.ClientId },
                { "redirect_uri", redirectUri },
                { "client_secret", this.Options.ClientSecret },
                { "code", code },
                { "grant_type" , "authorization_code" },
            };

            var requestContent = new FormUrlEncodedContent(tokenRequestParameters);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, this.Options.TokenEndpoint);
            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            requestMessage.Content = requestContent;

            var response = await Backchannel.SendAsync(requestMessage, context.RequestAborted);

            if (response.IsSuccessStatusCode)
            {
                var payload = JObject.Parse(await response.Content.ReadAsStringAsync());
                return OAuthTokenResponse.Success(payload);
            }
            else
            {
                var error = "OAuth token endpoint failure: " + await Display(response);
                return OAuthTokenResponse.Failed(new Exception(error));
            }
        }

        protected async Task ProcessError(HttpContext context)
        {
            var query = context.Request.Query;

            var error = query["error"];
            var failureMessage = new StringBuilder();
            failureMessage.Append(error);
            var errorDescription = query["error_description"];
            if (!StringValues.IsNullOrEmpty(errorDescription))
            {
                failureMessage.Append(";Description=").Append(errorDescription);
            }
            var errorUri = query["error_uri"];
            if (!StringValues.IsNullOrEmpty(errorUri))
            {
                failureMessage.Append(";Uri=").Append(errorUri);
            }

            if (this.Options.OnOAuthFailure != null)
                await this.Options.OnOAuthFailure(failureMessage.ToString(), this.AuthScheme);
        }

        protected async Task ProcessStateError(HttpContext context)
        {
            if (this.Options.OnOAuthFailure != null)
                await this.Options.OnOAuthFailure("Login failed. Wrong state", this.AuthScheme);
        }

        protected async Task ProcessCustomError(string error)
        {
            if(this.Options.OnOAuthFailure != null)
                await this.Options.OnOAuthFailure(error, this.AuthScheme);
        }

        protected string BuildRedirectUri(HttpContext context)
        {
            return context.Request.Scheme + "://" + context.Request.Host + @"/" + this.Options.CallbackPath;
        }

        protected virtual string GenerateState()
        {
            long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            string message = string.Format("{1}:{0}", milliseconds.ToString(), StateSecret);
            string password = Convert.ToBase64String(this.uniqueKey);
            var state = Encryption.AESThenHMAC.SimpleEncryptWithPassword(message, password);
            return state;
        }

        protected bool CheckOAuthState(StringValues values)
        {
            var state = values.ToString();
            string password = Convert.ToBase64String(this.uniqueKey);
            var message = Encryption.AESThenHMAC.SimpleDecryptWithPassword(state, password);
            var payload = message.Split(':');
            if(payload == null 
                || payload.Length != 2 
                || string.IsNullOrEmpty(payload[0])
                || payload[0] != StateSecret)
            {
                return false;
            }
            long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            long issued;
            if (!long.TryParse(payload[1], out issued)) return false;

            return Math.Abs(milliseconds - issued) < 100 * 1000;
        }

        private static async Task<string> Display(HttpResponseMessage response)
        {
            var output = new StringBuilder();
            output.Append("Status: " + response.StatusCode + ";");
            output.Append("Headers: " + response.Headers.ToString() + ";");
            output.Append("Body: " + await response.Content.ReadAsStringAsync() + ";");
            return output.ToString();
        }

        private void Validate()
        {
            if (this.Options == null) throw new ArgumentNullException(this.Options.GetType().ToString());
            if (string.IsNullOrEmpty(this.Options.ClientId)) throw new ArgumentNullException("ClientId");
            if (string.IsNullOrEmpty(this.Options.ClientSecret)) throw new ArgumentNullException("ClientSecret");
            if (string.IsNullOrEmpty(this.Options.CallbackPath)) throw new ArgumentNullException("CallbackEndpoint");
        }

    }
}