namespace BabyMonitoringApp.Platforms.Windows.Authentication
{
    public class OAuthConfig
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string AuthUrl { get; set; }
        public string TokenUrl { get; set; }
        public string RedirectUri { get; set; }
        public string Scope { get; set; }
        public string UserInfoUrl { get; set; }
        public bool TokenPost { get; set; } = true;

        public static OAuthConfig GetOAuthConfig(OAuthProvider provider)
        {
            return provider switch
            {
                OAuthProvider.Google => new OAuthConfig
                {
                    ClientId = "79037211679-dr0al5ce7nutsnbjdr7e7loj0m6nsh3j.apps.googleusercontent.com",
                    ClientSecret = "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",
                    AuthUrl = "https://accounts.google.com/o/oauth2/v2/auth",
                    TokenUrl = "https://oauth2.googleapis.com/token",
                    Scope = "openid email profile",
                    UserInfoUrl = "https://www.googleapis.com/oauth2/v3/userinfo",
                    TokenPost = true
                },
                OAuthProvider.Facebook => new OAuthConfig
                {
                    ClientId = "9832963710125227",
                    ClientSecret = "c86e9823073e424b41dca805caeab23b",
                    AuthUrl = "https://www.facebook.com/v18.0/dialog/oauth",
                    TokenUrl = "https://graph.facebook.com/v18.0/oauth/access_token",
                    Scope = "email public_profile",
                    UserInfoUrl = "https://graph.facebook.com/me?fields=id,name,email,picture",
                    TokenPost = false
                },
                _ => throw new NotSupportedException()
            };
        }
    }


    public enum OAuthProvider
    {
        Google,
        Facebook
    }
}
