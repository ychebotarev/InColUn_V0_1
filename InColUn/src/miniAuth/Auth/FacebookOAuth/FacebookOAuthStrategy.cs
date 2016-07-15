using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;

namespace miniAuth.FacebookOAuth
{
    public class FacebookOAuthStrategy : OAuthStrategy<FacebookStrategyOptions>
    {
        public FacebookOAuthStrategy(FacebookStrategyOptions options):base(options)
        {
            
        }

        public override string AuthScheme => FacebookDefaults.AuthenticationScheme;

        protected override async Task<ClaimsIdentity> GetUserClaimsAsync(HttpContext context, OAuthTokenResponse token)
        {
            var endpoint = QueryHelpers.AddQueryString(FacebookDefaults.UserInformationEndpoint, "access_token", token.AccessToken);
            if (Options.Fields.Count > 0)
            {
                endpoint = QueryHelpers.AddQueryString(endpoint, "fields", string.Join(",", Options.Fields));
            }
            var response = await Backchannel.GetAsync(endpoint, context.RequestAborted);
            response.EnsureSuccessStatusCode();

            var payload = JObject.Parse(await response.Content.ReadAsStringAsync());

            var identity = new ClaimsIdentity();
            var identifier = FacebookHelper.GetId(payload);
            if (!string.IsNullOrEmpty(identifier))
            {
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, identifier, ClaimValueTypes.String, FacebookDefaults.AuthenticationScheme));
            }

            var ageRangeMin = FacebookHelper.GetAgeRangeMin(payload);
            if (!string.IsNullOrEmpty(ageRangeMin))
            {
                identity.AddClaim(new Claim("urn:facebook:age_range_min", ageRangeMin, ClaimValueTypes.String, FacebookDefaults.AuthenticationScheme));
            }

            var ageRangeMax = FacebookHelper.GetAgeRangeMax(payload);
            if (!string.IsNullOrEmpty(ageRangeMax))
            {
                identity.AddClaim(new Claim("urn:facebook:age_range_max", ageRangeMax, ClaimValueTypes.String, FacebookDefaults.AuthenticationScheme));
            }

            var birthday = FacebookHelper.GetBirthday(payload);
            if (!string.IsNullOrEmpty(birthday))
            {
                identity.AddClaim(new Claim(ClaimTypes.DateOfBirth, birthday, ClaimValueTypes.String, FacebookDefaults.AuthenticationScheme));
            }

            var email = FacebookHelper.GetEmail(payload);
            if (!string.IsNullOrEmpty(email))
            {
                identity.AddClaim(new Claim(ClaimTypes.Email, email, ClaimValueTypes.String, FacebookDefaults.AuthenticationScheme));
            }

            var firstName = FacebookHelper.GetFirstName(payload);
            if (!string.IsNullOrEmpty(firstName))
            {
                identity.AddClaim(new Claim(ClaimTypes.GivenName, firstName, ClaimValueTypes.String, FacebookDefaults.AuthenticationScheme));
            }

            var gender = FacebookHelper.GetGender(payload);
            if (!string.IsNullOrEmpty(gender))
            {
                identity.AddClaim(new Claim(ClaimTypes.Gender, gender, ClaimValueTypes.String, FacebookDefaults.AuthenticationScheme));
            }

            var lastName = FacebookHelper.GetLastName(payload);
            if (!string.IsNullOrEmpty(lastName))
            {
                identity.AddClaim(new Claim(ClaimTypes.Surname, lastName, ClaimValueTypes.String, FacebookDefaults.AuthenticationScheme));
            }

            var link = FacebookHelper.GetLink(payload);
            if (!string.IsNullOrEmpty(link))
            {
                identity.AddClaim(new Claim("urn:facebook:link", link, ClaimValueTypes.String, FacebookDefaults.AuthenticationScheme));
            }

            var location = FacebookHelper.GetLocation(payload);
            if (!string.IsNullOrEmpty(location))
            {
                identity.AddClaim(new Claim("urn:facebook:location", location, ClaimValueTypes.String, FacebookDefaults.AuthenticationScheme));
            }

            var locale = FacebookHelper.GetLocale(payload);
            if (!string.IsNullOrEmpty(locale))
            {
                identity.AddClaim(new Claim(ClaimTypes.Locality, locale, ClaimValueTypes.String, FacebookDefaults.AuthenticationScheme));
            }

            var middleName = FacebookHelper.GetMiddleName(payload);
            if (!string.IsNullOrEmpty(middleName))
            {
                identity.AddClaim(new Claim("urn:facebook:middle_name", middleName, ClaimValueTypes.String, FacebookDefaults.AuthenticationScheme));
            }

            var name = FacebookHelper.GetName(payload);
            if (!string.IsNullOrEmpty(name))
            {
                identity.AddClaim(new Claim(ClaimTypes.Name, name, ClaimValueTypes.String, FacebookDefaults.AuthenticationScheme));
            }

            var timeZone = FacebookHelper.GetTimeZone(payload);
            if (!string.IsNullOrEmpty(timeZone))
            {
                identity.AddClaim(new Claim("urn:facebook:timezone", timeZone, ClaimValueTypes.String, FacebookDefaults.AuthenticationScheme));
            }

            return identity;
        }
    }
}