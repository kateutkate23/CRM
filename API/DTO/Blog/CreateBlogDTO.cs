using System.ComponentModel.DataAnnotations;

namespace API.DTO.Blog
{
    public class CreateBlogDTO
    {
        [Required(ErrorMessage = "Необходим добавить заголовок блога!")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Необходимо добавить описание!")]
        public string? Description { get; set; }
    }
}
