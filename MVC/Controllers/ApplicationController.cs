using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.ViewModels;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace MVC.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly HttpClient _client;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ApplicationController(HttpClient client, IWebHostEnvironment hostingEnvironment)
        {
            _client = client;
            _client.BaseAddress = new Uri("https://localhost:7246/api/");
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "Resources", "mainTitle.txt");

            if (System.IO.File.Exists(filePath))
            {
                string mainTitle = System.IO.File.ReadAllText(filePath);
                ViewData["MainTitle"] = mainTitle;
            }
            else
            {
                ViewData["MainTitle"] = "IT-консалтинг без СМС и регистрации";
            }

            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ChangeTitle()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult ChangeTitle(MainTitle mainTitle)
        {
            string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "Resources", "mainTitle.txt");

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.WriteAllText(filePath, mainTitle.Title);
            }

            ViewData["MainTitle"] = mainTitle.Title;

            return View("Index");
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
        public async Task<IActionResult> ViewInPeriod(string selectedPeriod)
        {
            string? token = Request.Cookies["AuthToken"];
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string begin;
            string end;

            switch (selectedPeriod)
            {
                case "today":
                    begin = DateTime.Now.ToShortDateString();
                    end = DateTime.Now.ToShortDateString();
                    break;
                case "yesterday":
                    begin = DateTime.Now.AddDays(-1).ToShortDateString();
                    end = DateTime.Now.AddDays(-1).ToShortDateString();
                    break;
                case "this week":
                    begin = DateTime.Now.AddDays(-7).ToShortDateString();
                    end = DateTime.Now.ToShortDateString();
                    break;
                default:
                    begin = DateTime.MinValue.ToShortDateString();
                    end = DateTime.Now.ToShortDateString();
                    break;
            }

            var response = await _client.GetFromJsonAsync<List<Application>>($"application/between/{begin}/{end}");

            return View("ViewAll", response);
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
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateApplicationVM vm)
        {
            string? token = Request.Cookies["AuthToken"];
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(vm);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            Console.WriteLine(json);

            var response = await _client.PutAsync($"application/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ViewAll");
            }

            return View("Error");
        }
    }
}
