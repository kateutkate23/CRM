using System.ComponentModel.DataAnnotations;

namespace WPF.ViewModels.Content
{
    public class LoginVM
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
