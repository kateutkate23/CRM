using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using WPF.Helpers;
using WPF.Models;

namespace WPF.ViewModels
{
    public class BlogVM : VMBase
    {
        private readonly HttpClient _client;
        private ObservableCollection<Blog>? _blogsCollection;
        private Blog? _selectedBlog;
        public Blog? SelectedBlog
        {
            get { return _selectedBlog; }
            set
            {
                _selectedBlog = value;
                OnPropertyChanged();
            }
        }
        public BlogVM()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7246/api/")
            };
            Task _ = GetAllBlogsAsync();
        }
        public BlogVM(Blog selected)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7246/api/")
            };
            SelectedBlog = selected;
        }

        public ObservableCollection<Blog>? BlogsCollection
        {
            get { return _blogsCollection; }
            set
            {
                _blogsCollection = value;
                OnPropertyChanged();
            }
        }

        public async Task GetAllBlogsAsync()
        {
            var response = await _client.GetFromJsonAsync<List<Blog>>("blog");

            BlogsCollection = response == null ? [] : new ObservableCollection<Blog>(response);
        }
    }
}
