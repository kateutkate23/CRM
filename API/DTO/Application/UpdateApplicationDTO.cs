using CRM.Models;

namespace API.DTO.Application
{
    public class UpdateApplicationDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Message { get; set; }
        public ApplicationStatus Status { get; set; }
    }
}
