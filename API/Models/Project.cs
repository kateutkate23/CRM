using System.ComponentModel.DataAnnotations;

namespace CRM.Models
{
    public class Project
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Необходимо добавить заголовок проекта!")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Необходимо добавить изображение!")]
        public string? ImageURL { get; set; }
        [Required(ErrorMessage = "Необходимо добавить описание проекта!")]
        public string? Description { get; set; }
    }
}