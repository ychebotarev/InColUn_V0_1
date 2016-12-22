namespace AuthLib.GoogleOAuth
{
    public class GoogleStrategyOptions : OAuthStrategyOptions
    {
        public static GoogleStrategyOptions CreateDefault()
        {
            return new GoogleStrategyOptions
            {
                AuthorizationEndpoint = GoogleDefaults.AuthorizationEndpoint,
                TokenEndpoint = GoogleDefaults.TokenEndpoint,
                UserInformationEndpoint = GoogleDefaults.UserInformationEndpoint
            };
        }

        public GoogleStrategyOptions()
        {
            Scope.Add("openid");
            Scope.Add("profile");
            Scope.Add("email");
        }
    }
}
