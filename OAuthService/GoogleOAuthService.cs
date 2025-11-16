using Microsoft.Extensions.Options;
using OAuthService.Options;
using System.Web;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OAuthService
{
    public class GoogleOAuthService(ApiClientHelper apiClientHelper, IOptions<GoogleOAuthOptions> options) : IOAuthService
    {

        public OAuthProviderEnum Provider => OAuthProviderEnum.Google;

        public string GetAuthorizationUrl(string state)
        {
            var query = new Dictionary<string, string>
    {
        { "client_id", options.Value.ClientId },
        { "redirect_uri", options.Value.RedirectUrl },
        { "response_type", "code" },
        { "scope", options.Value.Scope },
        { "state", state }
    };

            var url = options.Value.AuthUrl + "?" + string.Join("&",
                query.Select(kv => $"{kv.Key}={Uri.EscapeDataString(kv.Value)}")
            );

            return url;
        }

        public async Task<OAuthResponse> OAuthCallBack(string state, string code = "", string error = "")
        {
            if (options?.Value == null)
                return new OAuthResponse(false, "Options config is empty", null);

            if (!string.IsNullOrEmpty(error))
                return new OAuthResponse(false, error, null);


            var values = new Dictionary<string, string>
    {
        { "code", code },
        { "client_id", options.Value.ClientId },
        { "client_secret", options.Value.ClientSecret },
        { "redirect_uri", options.Value.RedirectUrl },
        { "grant_type", "authorization_code" }
    };

            GoogleTokenResponse? tokenResp;
            try
            {
                tokenResp = await apiClientHelper.PostAsync<GoogleTokenResponse>(
                    options.Value.TokenUrl, values, "application/x-www-form-urlencoded");
            }
            catch (Exception ex)
            {
                return new OAuthResponse(false, $"Failed to exchange code for token: {ex.Message}", null);
            }

            if (string.IsNullOrWhiteSpace(tokenResp?.Access_token))
                return new OAuthResponse(false, "Invalid or empty access token", null);

            try
            {
                apiClientHelper.SetBearerToken(tokenResp.Access_token);
                var info = await apiClientHelper.GetAsync<GoogleUserInfo>(options.Value.UserInfoUrl);

                if (info == null)
                    return new OAuthResponse(false, "Failed to retrieve user info", null);

                var oAuthInfo = new OAuthUserInfo(info, this.Provider);
                return new OAuthResponse(true, string.Empty, oAuthInfo);
            }
            catch (Exception ex)
            {
                return new OAuthResponse(false, $"Failed to retrieve user info: {ex.Message}", null);
            }
        }



    }
}
