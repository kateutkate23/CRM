using System.ComponentModel.DataAnnotations;
using WPF.Models;

namespace WPF.ViewModels.Content
{
    public class EditApplicationVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Необходимо ввести имя!")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Необходимо ввести email адрес!")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Необходимо оставить сообщение!")]
        public string? Message { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required]
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Received;
    }
}
