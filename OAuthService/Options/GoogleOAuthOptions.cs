using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuthService.Options
{
    public class GoogleOAuthOptions : BaseOAuthConfigOptions
    {
        public string ClientId { get; set; } = "";
        public string ClientSecret { get; set; } = "";
        public string Scope { get; set; } = "";
    }
}
