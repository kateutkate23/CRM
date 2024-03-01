using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using WPF.ViewModels.Content;

namespace WPF.ViewModels
{
    public class AccountVM
    {
        private readonly HttpClient _httpClient;
        public AccountVM()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7246/api/user/")
            };
        }

        public async Task<string?> SignInAsync(string username, string password)
        {
            LoginVM loginVM = new()
            {
                Username = username,
                Password = password
            };
            var response = await _httpClient.PostAsJsonAsync("login", loginVM);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var token = ExtractTokenFromJson(jsonResponse);

                if (token == null)
                {
                    return null;
                }

                return token;
            }
            else
            {
                return null;
            }
        }
        private static string? ExtractTokenFromJson(string json)
        {
            JToken token = JToken.Parse(json);
            return token["token"]?.ToString();
        }
    }
}
