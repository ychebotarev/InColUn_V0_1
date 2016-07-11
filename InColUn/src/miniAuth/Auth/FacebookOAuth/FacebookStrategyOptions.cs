using System.Collections.Generic;

namespace InColUn.Auth.FacebookOAuth
{
    public class FacebookStrategyOptions : OAuthStrategyOptions
    {
        public static FacebookStrategyOptions CreateDefault()
        {
            return new FacebookStrategyOptions
            {
                AuthorizationEndpoint = FacebookDefaults.AuthorizationEndpoint,
                TokenEndpoint = FacebookDefaults.TokenEndpoint,
                UserInformationEndpoint = FacebookDefaults.UserInformationEndpoint
            };
        }

        public FacebookStrategyOptions()
        {
            Scope.Add("public_profile");
            Scope.Add("email");
            Fields.Add("name");
            Fields.Add("email");
            Fields.Add("first_name");
            Fields.Add("last_name");
        }

        /// <summary>
        /// The list of fields to retrieve from the UserInformationEndpoint.
        /// https://developers.facebook.com/docs/graph-api/reference/user
        /// </summary>
        public ICollection<string> Fields { get; } = new HashSet<string>();

        public override string FormatScope()
        {
            // Facebook deviates from the OAuth spec here. They require comma separated instead of space separated.
            // https://developers.facebook.com/docs/reference/dialogs/oauth
            // http://tools.ietf.org/html/rfc6749#section-3.3
            return string.Join(",", this.Scope);
        }
    }
}
