using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.ViewModels;
using System.Security.Claims;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _client;
        public AccountController(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri("https://localhost:7246/api/");
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginVM loginVM)
        {
            var response = await _client.PostAsJsonAsync("user/login", loginVM);

            if (response.IsSuccessStatusCode)
            {
                var userString = await response.Content.ReadAsStringAsync();
                JToken userToken = JToken.Parse(userString);

                string tokenValue = (userToken["token"] ?? "").ToString();
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenValue);

                var token = new JwtSecurityTokenHandler().ReadJwtToken(tokenValue);

                var tokenClaims = token.Claims;

                var roleClaim = tokenClaims.FirstOrDefault(c => c.Type == "role");

                string userRole = String.Empty;
                if (roleClaim != null)
                {
                    userRole = roleClaim.Value;
                }

                var claims = new List<Claim>
                {
                    new(ClaimTypes.GivenName, loginVM.Username ?? ""),
                    new(ClaimTypes.Role, userRole)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                Response.Cookies.Append("AuthToken", tokenValue, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties());

                return RedirectToAction("Index", "Application");
            }

            ModelState.AddModelError(string.Empty, "Invalid username or password");
            return View(loginVM);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _client.DefaultRequestHeaders.Authorization = null;
            Response.Cookies.Delete("AuthToken");

            return RedirectToAction("Index", "Application");
        }
    }
}
