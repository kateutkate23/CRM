using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class ImageController : Controller
    {
        public IActionResult GetImage(string imageName)
        {
            string fullPath = "C:\\Users\\1\\Desktop\\skillbox\\C#\\CRM\\API\\" + imageName;
            byte[] imageBytes = System.IO.File.ReadAllBytes(fullPath);
            return File(imageBytes, "image/jpg");
        }
    }
}
