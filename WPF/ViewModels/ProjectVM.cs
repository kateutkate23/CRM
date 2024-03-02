using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using WPF.Helpers;
using WPF.Models;

namespace WPF.ViewModels
{
    public class ProjectVM : VMBase
    {
        private readonly HttpClient _client;
        private ObservableCollection<Project>? _projectsCollection;
        private Project? _selectedProject;
        public string? Token { get; set; }
        public Project? SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                _selectedProject = value;
                OnPropertyChanged();
            }
        }

        public ProjectVM()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7246/api/")
            };
            Task _ = GetAllProjectsAsync();
        }
        public ProjectVM(string? token)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7246/api/")
            };
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            Task _ = GetAllProjectsAsync();
            Token = token;
        }
        public ProjectVM(Project selected)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7246/api/")
            };
            SelectedProject = selected;
        }

        public ObservableCollection<Project>? ProjectsCollection
        {
            get { return _projectsCollection; }
            set
            {
                _projectsCollection = value;
                OnPropertyChanged();
            }
        }
        public async Task<bool> EditProject(Project selected, string? newTitle, string? newDescription, string? newURL)
        {
            using var formData = new MultipartFormDataContent
            {
                { new StringContent(newTitle ?? ""), "Title" },
                { new StringContent(newDescription ?? ""), "Description" }
            };

            if (!string.IsNullOrEmpty(newURL))
            {
                byte[] imageData = File.ReadAllBytes(newURL);

                ByteArrayContent imageContent = new ByteArrayContent(imageData);
                imageContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

                formData.Add(imageContent, "img", Path.GetFileName(newURL));
            }

            var request = new HttpRequestMessage(HttpMethod.Put, $"project/{selected.Id}")
            {
                Content = formData
            };

            request.Headers.TransferEncodingChunked = true;

            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteProject (Project project)
        {
            var response = await _client.DeleteAsync($"project/{project.Id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> AddProject(string? title, string? description, string? imageUrl)
        {
            using var formData = new MultipartFormDataContent
            {
                { new StringContent(title ?? ""), "Title" },
                { new StringContent(description ?? ""), "Description" }
            };

            if (!string.IsNullOrEmpty(imageUrl))
            {
                byte[] imageData = File.ReadAllBytes(imageUrl);
                ByteArrayContent imageContent = new(imageData);
                formData.Add(imageContent, "img", Path.GetFileName(imageUrl));
            }

            var response = await _client.PostAsync("project", formData);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task GetAllProjectsAsync()
        {
            var response = await _client.GetFromJsonAsync<List<Project>>("project");

            ProjectsCollection = response == null ? [] : new ObservableCollection<Project>(response);
        }
    }
}
