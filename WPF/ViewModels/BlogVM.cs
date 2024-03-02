using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
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
        public string? Token { get; set; }
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
        public BlogVM(string? token)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7246/api/")
            };
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            Task _ = GetAllBlogsAsync();
            Token = token;
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

        public async Task<bool> EditBlog(Blog selected, string? newTitle, string? newDescription, string? newURL)
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

            var request = new HttpRequestMessage(HttpMethod.Put, $"blog/{selected.Id}")
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

        public async Task<bool> DeleteBlog(Blog blog)
        {
            var response = await _client.DeleteAsync($"blog/{blog.Id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> AddBlog(string? title, string? description, string? imageUrl)
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

            var response = await _client.PostAsync("blog", formData);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task GetAllBlogsAsync()
        {
            var response = await _client.GetFromJsonAsync<List<Blog>>("blog");

            BlogsCollection = response == null ? [] : new ObservableCollection<Blog>(response);
        }
    }
}
