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
        public string Provider => "Google";
      /*  public string Provider
        {
            get
            {
                return "Google";
            }
        }*/
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

        public async Task<OAuthUserInfo> OAuthCallBack(string state, string code = "", string error = "")
        {
            if (options == null || options.Value == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            if (!string.IsNullOrEmpty(error))
            {
                throw new Exception(error);
            }
            var values = new Dictionary<string, string>
{
    { "code", code },
    { "client_id", options.Value.ClientId },
    { "client_secret", options.Value.ClientSecret },
    { "redirect_uri", options.Value.RedirectUrl },
    { "grant_type", "authorization_code" }
};
            var response = await apiClientHelper.PostAsync<GoogleTokenResponse>(options.Value.TokenUrl, values, "application/x-www-form-urlencoded");
            if (response == null)
            {
                throw new Exception("get token fail");
            }
            apiClientHelper.SetBearerToken(response.Access_token);

            var info = await apiClientHelper.GetAsync<GoogleUserInfo>(options.Value.UserInfoUrl);
            return new OAuthUserInfo(info, this.Provider);
        }
    }
}
