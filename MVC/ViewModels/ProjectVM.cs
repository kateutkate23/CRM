using System.ComponentModel.DataAnnotations;

namespace MVC.ViewModels
{
    public class ProjectVM
    {
        [Required(ErrorMessage = "Необходимо добавить заголовок проекта!")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Необходимо добавить изображение!")]
        public IFormFile? Image { get; set; }
        [Required(ErrorMessage = "Необходимо добавить описание проекта!")]
        public string? Description { get; set; }
    }
}
