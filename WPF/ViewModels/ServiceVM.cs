using Azure;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using WPF.Helpers;
using WPF.Models;

namespace WPF.ViewModels
{
    public class ServiceVM : VMBase
    {
        private readonly HttpClient _client;
        private ObservableCollection<Service>? _servicesCollection;
        public Service? Selected { get; set; }
        public string? Token { get; set; }
        public ServiceVM()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7246/api/")
            };
            Task _ = GetAllServicesAsync();
        }
        public ServiceVM(string? token)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7246/api/")
            };
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            Token = token;
            Task _ = GetAllServicesAsync();
        }

        public ObservableCollection<Service>? ServicesCollection
        {
            get { return _servicesCollection; }
            set
            {
                _servicesCollection = value;
                OnPropertyChanged();
            }
        }

        public async Task<bool> EditService(Service selected, string? newTitle, string? newDescription)
        {
            Service edited = new()
            {
                Id = selected.Id,
                Title = newTitle,
                Description = newDescription
            };

            var response = await _client.PutAsJsonAsync($"service/{edited.Id}", edited);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> AddService(string title, string description)
        {
            Service service = new()
            {
                Title = title,
                Description = description
            };
            var response = await _client.PostAsJsonAsync("service", service);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteService(Service service)
        {
            var response = await _client.DeleteAsync($"service/{service.Id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task GetAllServicesAsync()
        {
            var response = await _client.GetFromJsonAsync<List<Service>>("service");

            ServicesCollection = response == null ? [] : new ObservableCollection<Service>(response);
        }
    }
}
