using System.Net.Http;
using System.Net.Http.Json;
using WPF.Helpers;
using WPF.ViewModels.Content;

namespace WPF.ViewModels
{
    public class ApplicationVM : VMBase
    {
        private readonly HttpClient _client;
        public ApplicationVM() 
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7246/api/")
            };
        }

        public async Task<bool> PostApplicationAsync(string? name, string? email, string? message)
        {
            if (name == null || email == null || message == null)
            {
                return false;
            }

            AddApplicationVM vm = new()
            {
                Name = name,
                Email = email,
                Message = message
            };

            var response = await _client.PostAsJsonAsync("application", vm);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
