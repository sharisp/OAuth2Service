using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
namespace OAuthService
{


    public class ApiClientHelper(HttpClient httpClient)
    {
        public void SetBearerToken(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<T?> GetAsync<T>(string url)
        {
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<T?> PostAsync<T>(string url, object data,string contentType= "application/json")
        {
            HttpContent content;
            ;
            if (contentType == "application/x-www-form-urlencoded" && data is Dictionary<string, string> dict)
            {
                content = new FormUrlEncodedContent(dict);
            }
            else
            {
                var json = JsonSerializer.Serialize(data);
                content = new StringContent(json, Encoding.UTF8, contentType);
            }

            var response = await httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<T?> PutAsync<T>(string url, object data)
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task DeleteAsync(string url)
        {
            var response = await httpClient.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
        }
    }

}
