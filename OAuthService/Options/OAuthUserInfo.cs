using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuthService.Options
{
    public class OAuthUserInfo
    {
        public string Provider { get; set; } = "";
        public string Id { get; set; } = "";
        public string Email { get; set; } = "";
        public string Name { get; set; } = "";
        public string Avatar { get; set; } = "";

        private OAuthUserInfo() { }
        public  OAuthUserInfo(GoogleUserInfo googleUserInfo, string provider)
        {
            //OAuthUserInfo oAuthUserInfo = new OAuthUserInfo();
            Id = googleUserInfo.Id;
            Provider = provider;

            Avatar = googleUserInfo.Picture;
            Email = googleUserInfo.Email;
            Name = googleUserInfo.Name;
           
          
        }
    }



}
