using API.DTO.Application;
using CRM.Models;

namespace API.Mappers
{
    public static class ApplicationMapper
    {
        public static Application ToApplicationFromDTO(this ApplicationDTO dto)
        {
            Application application = new()
            {
                Name = dto.Name,
                Email = dto.Email,
                Message = dto.Message,
                CreatedDate = dto.CreatedDate,
                Status = dto.Status
            };

            return application;
        }

        public static ApplicationDTO ToDTOFromApplication(this Application application)
        {
            ApplicationDTO dto = new()
            {
                Id = application.Id,
                Name = application.Name,
                Email = application.Email,
                Message = application.Message,
                CreatedDate = application.CreatedDate,
                Status = application.Status
            };

            return dto;
        }

        public static Application ToApplicationFromCreateDTO(this CreateApplicationDTO dto)
        {
            Application application = new()
            {
                Name = dto.Name,
                Email = dto.Email,
                Message = dto.Message,
                CreatedDate = DateTime.Now,
                Status = ApplicationStatus.Received
            };

            return application;
        }

        public static Application ToApplicationFromUpdateDTO(this Application application, UpdateApplicationDTO dto)
        {
            Application result = new()
            {
                Id = application.Id,
                Name = dto.Name,
                Email = dto.Email,
                Message = dto.Message,
                CreatedDate = application.CreatedDate,
                Status = dto.Status
            };

            return result;
        }
    }
}
