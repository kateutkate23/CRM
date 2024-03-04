using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.ViewModels;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace MVC.Controllers
{
    public class ServicesController : Controller
    {
        private readonly HttpClient _client;
        public ServicesController(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("https://localhost:7246/api/");
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(ServiceVM vm)
        {
            string? token = Request.Cookies["AuthToken"];
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.PostAsJsonAsync("service", vm);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Success");
            }

            return View("Error");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View();

        }
        [Authorize(Roles = "Admin")]
        public IActionResult Success()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetFromJsonAsync<List<Service>>("service");

            if (response != null)
            {
                return View(response);
            }

            return View("Error");
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            string? token = Request.Cookies["AuthToken"];
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetFromJsonAsync<Service>($"service/{id}");

            if (response != null)
            {
                return View("Edit", response);
            }

            return View("Error");
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateService(int id, UpdateServiceVM vm)
        {
            string? token = Request.Cookies["AuthToken"];
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(vm);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync($"service/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("Error");
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteService(int id)
        {
            string? token = Request.Cookies["AuthToken"];
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.DeleteAsync($"service/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View("Error");
        }
    }
}
