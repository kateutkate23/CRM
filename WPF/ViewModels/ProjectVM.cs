using System.Collections.ObjectModel;
using System.Net.Http;
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

        public async Task GetAllProjectsAsync()
        {
            var response = await _client.GetFromJsonAsync<List<Project>>("project");

            ProjectsCollection = response == null ? [] : new ObservableCollection<Project>(response);
        }
    }
}
