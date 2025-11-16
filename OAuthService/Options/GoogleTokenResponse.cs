using System.Text.Json.Serialization;

namespace OAuthService.Options
{

    public class GoogleTokenResponse
    {
        [JsonPropertyName("access_token")]
        public string Access_token { get; set; } = "";

        [JsonPropertyName("expires_in")]
        public int Expires_in { get; set; }

        [JsonPropertyName("refresh_token")]
        public string Refresh_token { get; set; } = "";

        [JsonPropertyName("scope")]
        public string Scope { get; set; } = "";

        [JsonPropertyName("token_type")]
        public string Token_type { get; set; } = "";

        [JsonPropertyName("id_token")]
        public string Id_token { get; set; } = "";
    }


}
