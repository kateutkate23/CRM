using System.ComponentModel.DataAnnotations;

namespace API.DTO.Service
{
    public class CreateServiceDTO
    {
        [Required(ErrorMessage = "Необходимо добавить заголовок услуги!")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Необходимо добавить описание услуги!")]
        public string? Description { get; set; }
    }
}
