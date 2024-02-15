using System.ComponentModel.DataAnnotations;

namespace CRM.Models
{
    public enum ApplicationStatus
    {
        Received,
        AtWork,
        Completed,
        Rejected,
        Cancelled
    }
    public class Application
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Необходимо ввести имя!")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Необходимо ввести email адрес!")]
        [EmailAddress]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Необходимо оставить сообщение!")]
        public string? Message { get; set; }
        public DateTime CreatedDate {  get; set; } = DateTime.Now;
        [Required]
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Received;
    }
}