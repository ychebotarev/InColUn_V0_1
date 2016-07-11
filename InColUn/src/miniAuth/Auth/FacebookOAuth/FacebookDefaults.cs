namespace InColUn.Auth.FacebookOAuth
{
    public static class FacebookDefaults
    {
        public const string AuthenticationScheme = "Facebook";

        public static readonly string AuthorizationEndpoint = "https://www.facebook.com/v2.6/dialog/oauth";

        public static readonly string TokenEndpoint = "https://graph.facebook.com/v2.6/oauth/access_token";

        public static readonly string UserInformationEndpoint = "https://graph.facebook.com/v2.6/me";
    }
}
