using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuthService.Options
{
    public class OAuthResponse
    {
        public bool Success { get; set; }
        public string ErrorMsg { get; set; }
        public OAuthUserInfo? UserInfo { get; set; }

        public OAuthResponse(bool success, string errorMsg, OAuthUserInfo? userInfo)
        {
            Success = success;
            ErrorMsg = errorMsg;
            UserInfo = userInfo;
        }
    }
}
