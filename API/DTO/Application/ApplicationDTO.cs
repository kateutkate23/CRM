using CRM.Models;

namespace API.DTO.Application
{
    public class ApplicationDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Message { get; set; }
        public DateTime CreatedDate { get; set; } 
        public ApplicationStatus Status { get; set; } 
    }
}
