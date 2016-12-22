using System.Security.Claims;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

using Newtonsoft.Json.Linq;

namespace AuthLib.GoogleOAuth
{
    public class GoogleOAuthStrategy : OAuthStrategy<GoogleStrategyOptions>
    {
        public GoogleOAuthStrategy(GoogleStrategyOptions options) : base(options)
        {
        }

        public override string AuthScheme => GoogleDefaults.AuthenticationScheme;

        protected override string BuildChallengeUrl(HttpContext context)
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
                { "access_type", "online" },
                { "approval_prompt", "auto" },
            };

            return this.Options.AuthorizationEndpoint + queryBuilder.ToString();
        }

        protected override async Task<ClaimsIdentity> GetUserClaimsAsync(HttpContext context, OAuthTokenResponse token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Options.UserInformationEndpoint);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            var response = await Backchannel.SendAsync(request, context.RequestAborted);
            response.EnsureSuccessStatusCode();

            var payload = JObject.Parse(await response.Content.ReadAsStringAsync());

            var identity = new ClaimsIdentity();
            var identifier = GoogleHelper.GetId(payload);
            if (!string.IsNullOrEmpty(identifier))
            {
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, identifier, ClaimValueTypes.String, this.AuthScheme));
            }

            var email = GoogleHelper.GetEmail(payload);
            if (!string.IsNullOrEmpty(email))
            {
                identity.AddClaim(new Claim(ClaimTypes.Email, email, ClaimValueTypes.String, this.AuthScheme));
            }

            var name = GoogleHelper.GetName(payload);
            if (!string.IsNullOrEmpty(name))
            {
                identity.AddClaim(new Claim(ClaimTypes.Name, name, ClaimValueTypes.String, this.AuthScheme));
            }

            return identity;
        }
    }
}
