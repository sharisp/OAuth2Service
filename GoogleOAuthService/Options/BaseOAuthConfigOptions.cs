using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuthService.Options
{
    public class BaseOAuthConfigOptions
    {
        public string AuthUrl { get; set; } = "";
        public string TokenUrl { get; set; } = "";
        public string UserInfoUrl { get; set; } = "";
        public string RedirectUrl { get; set; } = "";
    }
}
