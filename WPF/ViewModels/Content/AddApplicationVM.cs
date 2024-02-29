using WPF.Models;

namespace WPF.ViewModels.Content
{
    public class AddApplicationVM
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Message { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Received;
    }
}
