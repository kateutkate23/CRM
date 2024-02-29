using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using WPF.Helpers;
using WPF.Models;

namespace WPF.ViewModels
{
    public class ServiceVM : VMBase
    {
        private readonly HttpClient _client;
        private ObservableCollection<Service>? _servicesCollection;
        public ServiceVM()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7246/api/")
            };
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

        public async Task GetAllServicesAsync()
        {
            var response = await _client.GetFromJsonAsync<List<Service>>("service");

            ServicesCollection = response == null ? [] : new ObservableCollection<Service>(response);
        }
    }
}
