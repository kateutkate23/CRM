using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ContactsController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "Resources", "contacts.txt");

            if (System.IO.File.Exists(filePath))
            {
                string mainTitle = System.IO.File.ReadAllText(filePath);
                ViewData["Contacts"] = mainTitle;
            }
            else
            {
                ViewData["Contacts"] = "Санкт-Петербург\nпроспект Маршала Жукова, 24\nтелефон: +79959621755";
            }

            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult ChangeContacts()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult ChangeContacts(Contacts contacts)
        {
            string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "Resources", "contacts.txt");

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.WriteAllText(filePath, contacts.ContactsText);
            }

            ViewData["Contacts"] = contacts.ContactsText;

            return View("Index");
        }
    }
}
