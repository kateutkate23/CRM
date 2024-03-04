using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.ViewModels;
using System.Net.Http.Headers;

namespace MVC.Controllers
{
    public class BlogController : Controller
    {
        private readonly HttpClient _client;
        public BlogController(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("https://localhost:7246/api/");
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            string? token = Request.Cookies["AuthToken"];
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.DeleteAsync($"blog/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("Error");
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            string? token = Request.Cookies["AuthToken"];
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetFromJsonAsync<Blog>($"blog/{id}");

            if (response != null)
            {
                UpdateBlogVM vm = new()
                {
                    Id = id,
                    Title = response.Title,
                    Description = response.Description,
                    ImageURL = response.ImageURL,
                };
                return View("Edit", vm);
            }

            return View("Error");
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBlog(int id, UpdateBlogVM vm)
        {
            string? token = Request.Cookies["AuthToken"];
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            if (vm.Image != null)
            {
                using var formData = new MultipartFormDataContent
                {
                    { new StringContent(vm.Title ?? ""), "Title" },
                    { new StringContent(vm.Description ?? ""), "Description" },
                    { new StreamContent(vm.Image.OpenReadStream()), "img", vm.Image.FileName }
                };
                var response = await _client.PutAsync($"blog/{id}", formData);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View("Error");
            }
            
            return View("Error");
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(BlogVM vm)
        {
            string? token = Request.Cookies["AuthToken"];
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            if (vm.Image != null)
            {
                using var formData = new MultipartFormDataContent
                {
                    { new StringContent(vm.Title ?? ""), "Title" },
                    { new StringContent(vm.Description ?? ""), "Description" },
                    { new StreamContent(vm.Image.OpenReadStream()), "img", vm.Image.FileName }
                };

                var response = await _client.PostAsync("blog", formData);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Success");
                }
            }
            else
            {
                ModelState.AddModelError("Image", "Добавьте изображение!");
                return BadRequest(ModelState);
            }

            return View("Error");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Success()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> View(int id)
        {
            string? token = Request.Cookies["AuthToken"];
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetFromJsonAsync<Blog>($"blog/{id}");

            if (response != null)
            {
                return View("View", response);
            }
            
            return View("Error");
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetFromJsonAsync<List<Blog>>("blog");

            if (response != null)
            {
                return View(response);
            }

            return View("Error");
        }
       
    }
}
