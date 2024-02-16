using API.DTO.Project;
using CRM.Models;

namespace API.Mappers
{
    public static class ProjectMapper
    {
        public static ProjectDTO ToDTOFromProject(this Project project)
        {
            ProjectDTO dto = new()
            {
                Id = project.Id,
                Title = project.Title,
                ImageURL = project.ImageURL,
                Description = project.Description
            };

            return dto;
        }

        public static Project ToProjectFromCreateDTO(this CreateProjectDTO dto)
        {
            Project project = new()
            {
                Title = dto.Title,
                Description = dto.Description
            };

            return project;
        }

        public static Project ToProjectFromUpdateDTO(this Project project, UpdateProjectDTO dto)
        {
            Project result = new()
            {
                Id = project.Id,
                Title = dto.Title ?? project.Title,
                ImageURL = project.ImageURL,
                Description = dto.Description ?? project.Description
            };

            return result;
        }
    }
}
