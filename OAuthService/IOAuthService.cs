using OAuthService.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuthService
{
    public interface IOAuthService
    {
        string Provider { get; }
        string GetAuthorizationUrl(string state);
        Task<OAuthUserInfo> OAuthCallBack(string state, string code = "", string error = "");
    }

}
