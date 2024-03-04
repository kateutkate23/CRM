using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class BlogVM
    {
        [Required(ErrorMessage = "Необходим добавить заголовок блога!")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Необходимо добавить изображение!")]
        public IFormFile? Image { get; set; }
        [Required(ErrorMessage = "Необходимо добавить описание!")]
        public string? Description { get; set; }
    }
}
