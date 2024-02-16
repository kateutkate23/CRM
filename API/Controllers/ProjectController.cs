using API.DTO.Project;
using API.Helpers;
using API.Interfaces;
using API.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/project")]
    [ApiController]
    public class ProjectController(IProjectRepository repository) : ControllerBase
    {
        private readonly IProjectRepository _repository = repository;

        // guest, admin
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projects = await _repository.GetAllAsync();
            var projectsDTO = projects.Select(p => p.ToDTOFromProject());

            return Ok(projectsDTO);
        }

        // guest, admin
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var project = await _repository.GetByIdAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return Ok(project.ToDTOFromProject());
        }

        // admin
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateProjectDTO dto, IFormFile? img)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (img == null)
            {
                ModelState.AddModelError("img", "The image field is required.");
                return BadRequest(ModelState);
            }

            var project = dto.ToProjectFromCreateDTO();

            await img.SaveToImages(project);
            await _repository.CreateAsync(project);

            return CreatedAtAction(nameof(GetById), new { id = project.Id }, project.ToDTOFromProject());
        }

        // admin
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromForm] UpdateProjectDTO dto, IFormFile? img)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var project = await _repository.GetByIdNoTrackingAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            var updatedProject = project.ToProjectFromUpdateDTO(dto);
            
            if (img != null)
            {
                await img.SaveToImages(updatedProject);
            }

            await _repository.UpdateAsync(updatedProject);

            return Ok(updatedProject.ToDTOFromProject());
        }

        // admin
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var project = await _repository.GetByIdAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(project);
            FileHelper.DeleteFile(project.ImageURL);

            return NoContent();
        }
    }
}
