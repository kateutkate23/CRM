using API.DTO.Service;
using CRM.Models;

namespace API.Mappers
{
    public static class ServiceMapper
    {
        public static ServiceDTO ToDTOFromService(this Service service)
        {
            ServiceDTO dto = new()
            {
                Id = service.Id,
                Title = service.Title,
                Description = service.Description
            };

            return dto;
        }

        public static Service ToServiceFromCreateDTO(this CreateServiceDTO dto)
        {
            Service service = new()
            {
                Title = dto.Title,
                Description = dto.Description
            };

            return service;
        }

        public static Service ToServiceFromUpdateDTO(this Service service, UpdateServiceDTO dto)
        {
            Service result = new()
            {
                Id = service.Id,
                Title = dto.Title,
                Description = dto.Description
            };

            return result;
        }
    }
}
