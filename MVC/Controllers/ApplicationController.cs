using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.ViewModels;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MVC.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly HttpClient _client;
        public ApplicationController(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("https://localhost:7246/api/");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Send(ApplicationVM vm)
        {
            var response = await _client.PostAsJsonAsync("application", vm);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Success");
            }

            return View("Error");
        }

        [HttpGet]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> ViewAll()
        {
            string? token = Request.Cookies["AuthToken"];
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetFromJsonAsync<List<Application>>("application");

            if (response != null)
            {
                return View(response);
            }

            return View("Error");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> View(int id)
        {
            string? token = Request.Cookies["AuthToken"];
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetFromJsonAsync<Application>($"application/{id}");

            if (response != null)
            {
                return View("View", response);
            }

            return View("Error");
        }
    }
}
