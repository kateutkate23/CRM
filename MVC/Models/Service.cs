using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class Service
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Необходимо добавить заголовок услуги!")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Необходимо добавить описание услуги!")]
        public string? Description { get; set; }
    }
}
