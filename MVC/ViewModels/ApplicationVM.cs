using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class ApplicationVM
    {
        [Required(ErrorMessage = "Необходимо ввести имя!")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Необходимо ввести email адрес!")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Необходимо оставить сообщение!")]
        public string? Message { get; set; }
    }
}
