using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text.Json;
using System.Web;

namespace BabyMonitoringApp.Platforms.Windows.Authentication
{
    public static class OAuthLoginHandler
    {
        public static async Task<string> SignInWithOAuth(OAuthProvider provider)
        {
            var config = OAuthConfig.GetOAuthConfig(provider);
            config.RedirectUri = provider == OAuthProvider.Google ? "http://127.0.0.1:5000/" : "https://facebook-redirect-git-main-ahmeds-projects-62c95710.vercel.app/facebook-redirect.html";
            string state = Guid.NewGuid().ToString("N");

            var authUrl = $"{config.AuthUrl}?response_type=code" +
                          $"&client_id={config.ClientId}" +
                          $"&redirect_uri={Uri.EscapeDataString(config.RedirectUri)}" +
                          $"&scope={Uri.EscapeDataString(config.Scope)}" +
                          $"&state={state}" +
                          (provider == OAuthProvider.Google ? "&access_type=offline&prompt=consent" : "");

            var listener = new HttpListener();
            listener.Prefixes.Add("http://127.0.0.1:5000/");
            listener.Start();
            Process.Start(new ProcessStartInfo(authUrl) { UseShellExecute = true });

            var context = await listener.GetContextAsync();
            var parsed = HttpUtility.ParseQueryString(context.Request.Url.Query);
            var code = parsed.Get("code");
            var returnedState = parsed.Get("state");

            string html = "<html><body><h1>You may now close this window.</h1></body></html>";
            var buffer = System.Text.Encoding.UTF8.GetBytes(html);
            context.Response.ContentLength64 = buffer.Length;
            context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            context.Response.OutputStream.Close();
            listener.Stop();

            if (state != returnedState || string.IsNullOrEmpty(code))
                throw new Exception("State mismatch or code not received.");

            // Exchange token
            string accessToken;
            if (config.TokenPost)
            {
                var tokenRequest = new Dictionary<string, string>
                                    {
                                        { "code", code },
                                        { "client_id", config.ClientId },
                                        { "client_secret", config.ClientSecret },
                                        { "redirect_uri", config.RedirectUri },
                                        { "grant_type", "authorization_code" }
                                    };

                var client = new HttpClient();
                var tokenResponse = await client.PostAsync(config.TokenUrl, new FormUrlEncodedContent(tokenRequest));
                var tokenJson = await tokenResponse.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(tokenJson);
                string idToken = doc.RootElement.GetProperty("id_token").GetString();
                accessToken = doc.RootElement.GetProperty("access_token").GetString();

                // Decode user info from id_token (JWT)
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(idToken);
                var name = jwt.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
                var email = jwt.Claims.FirstOrDefault(c => c.Type == "email")?.Value;
                var picture = jwt.Claims.FirstOrDefault(c => c.Type == "picture")?.Value;

                return name;
            }
            else
            {
                var tokenUrl = $"{config.TokenUrl}?client_id={config.ClientId}&redirect_uri={Uri.EscapeDataString(config.RedirectUri)}&client_secret={config.ClientSecret}&code={code}";
                var client = new HttpClient();
                var response = await client.GetAsync(tokenUrl);
                var tokenJson = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(tokenJson);
                accessToken = doc.RootElement.GetProperty("access_token").GetString();

                // Get user info
                var userClient = new HttpClient();
                var userInfoResponse = await userClient.GetAsync($"{config.UserInfoUrl}&access_token={accessToken}");
                string userJson = await userInfoResponse.Content.ReadAsStringAsync();
                using var userJsonDoc = JsonDocument.Parse(userJson);
                string name = userJsonDoc.RootElement.GetProperty("name").GetString();
                return name;
            }
        }
    }
}
