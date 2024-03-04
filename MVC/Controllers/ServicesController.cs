using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Net.Http.Headers;

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
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //string? token = Request.Cookies["AuthToken"];
            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetFromJsonAsync<List<Service>>("service");

            if (response != null)
            {
                return View(response);
            }

            return View("Error");
        }
    }
}
