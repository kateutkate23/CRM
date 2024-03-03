using MVC.Models;

namespace MVC.ViewModels
{
    public class UpdateApplicationVM
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Message { get; set; }
        public ApplicationStatus Status { get; set; }
    }
}
