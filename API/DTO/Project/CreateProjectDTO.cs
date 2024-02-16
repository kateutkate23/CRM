using System.ComponentModel.DataAnnotations;

namespace API.DTO.Project
{
    public class CreateProjectDTO
    {
        [Required(ErrorMessage = "Необходимо добавить заголовок проекта!")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Необходимо добавить описание проекта!")]
        public string? Description { get; set; }
    }
}
