using System.ComponentModel.DataAnnotations;

namespace API.DTO.Application
{
    public class CreateApplicationDTO
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
