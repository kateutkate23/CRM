using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using WPF.Helpers;
using WPF.Models;
using WPF.ViewModels.Content;

namespace WPF.ViewModels
{
    public class ApplicationVM : VMBase
    {
        private readonly HttpClient _client;
        private string? _mainTitleText;
        private ObservableCollection<Application>? _applications; 
        public string? Token { get; set; }
        public string MainTitleText
        {
            get { return _mainTitleText ?? ""; }
            set
            {
                _mainTitleText = value;
                OnPropertyChanged(nameof(MainTitleText));
            }
        }
        public ObservableCollection<Application>? Applications
        {
            get { return _applications; }
            set
            {
                _applications = value;
                OnPropertyChanged();
            }
        }
        public ApplicationVM() 
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7246/api/")
            };
            string filePath = "C:\\Users\\1\\Desktop\\skillbox\\C#\\CRM\\WPF\\Resources\\mainTitle.txt";
            if (File.Exists(filePath))
            {
                MainTitleText = File.ReadAllText(filePath);
            }
        }

        public ApplicationVM(string? token)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7246/api/"),
            };
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string filePath = "C:\\Users\\1\\Desktop\\skillbox\\C#\\CRM\\WPF\\Resources\\mainTitle.txt";
            if (File.Exists(filePath))
            {
                MainTitleText = File.ReadAllText(filePath);
            }
            Token = token;
        }

        public async Task<bool> EditApplicationStatus(Application application, ApplicationStatus newStatus)
        {
            EditApplicationVM vm = new()
            {
                Id = application.Id,
                Name = application.Name,
                Email = application.Email,
                Message = application.Message,
                CreatedDate = application.CreatedDate,
                Status = newStatus
            };

            var response = await _client.PutAsJsonAsync($"application/{vm.Id}", vm);

            return response.IsSuccessStatusCode;
        }
        public async Task GetApplicationsInPeriod(string start, string end)
        {
            var response = await _client.GetFromJsonAsync<List<Application>>($"application/between/{start}/{end}");

            if (response != null)
            {
                Applications = new ObservableCollection<Application>(response);
            }
            else
            {
                Applications = [];
            }
        }

        public async Task GetAllApplications()
        {
            var response = await _client.GetFromJsonAsync<List<Application>>("application");

            if (response != null)
            {
                Applications = new ObservableCollection<Application>(response);
            }
            else
            {
                Applications = [];
            }
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

        public void EditTitle(string newText)
        {
            MainTitleText = newText;
            OnPropertyChanged(nameof(MainTitleText));
            string path = "C:\\Users\\1\\Desktop\\skillbox\\C#\\CRM\\WPF\\Resources\\mainTitle.txt";

            File.WriteAllText(path, newText);
        }
    }
}
