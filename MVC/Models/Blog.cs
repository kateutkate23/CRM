using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class Blog
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Необходим добавить заголовок блога!")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Необходимо добавить изображение!")]
        public string? ImageURL { get; set; }
        [Required(ErrorMessage = "Необходимо добавить описание!")]
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
