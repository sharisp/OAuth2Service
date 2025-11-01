using Microsoft.Extensions.Options;
using OAuthService.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuthService
{
    public class GoogleOAuthService(ApiClientHelper apiClientHelper, IOptions<GoogleOAuthOptions> options)
    {
        public async Task<GoogleUserInfo> OAuthCallBack(string state, string code = "", string error = "")
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
            return info;
        }
    }
}
