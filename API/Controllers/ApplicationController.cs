using API.DTO.Application;
using API.Interfaces;
using API.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/application")]
    [ApiController]
    public class ApplicationController(IApplicationRepository repository) : ControllerBase
    {
        private readonly IApplicationRepository _repository = repository;

        // admin
        [HttpGet]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applications = await _repository.GetAllAsync();
            var applicationsDTO = applications.Select(a => a.ToDTOFromApplication());

            return Ok(applicationsDTO);
        }

        // admin
        [HttpGet("between/{begin}/{end}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetBetweenDatesAsync([FromRoute] string begin, [FromRoute] string end)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!DateTime.TryParse(begin, out DateTime beginDate))
            {
                return BadRequest("Некорректный формат начальной даты!");
            }

            if (!DateTime.TryParse(end, out DateTime endDate))
            {
                return BadRequest("Некорректный формат конечной даты!");
            }

            var applications = await _repository.GetBetweenDatesAsync(beginDate, endDate);
            var applicationsDTO = applications.Select(a => a.ToDTOFromApplication());

            return Ok(applicationsDTO);
        }

        // admin
        [HttpGet("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var application = await _repository.GetByIdAsync(id); 

            if (application == null)
            {
                return NotFound();
            }

            return Ok(application.ToDTOFromApplication());
        }

        // guest, admin
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateApplicationDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var application = dto.ToApplicationFromCreateDTO();
            await _repository.CreateAsync(application);

            return CreatedAtAction(nameof(GetById), new { id = application.Id }, application.ToDTOFromApplication());
        }

        // admin
        [HttpPut]
        [Route("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id,[FromBody] UpdateApplicationDTO dto)
        {
            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState); 
            }

            var application = await _repository.GetByIdNoTrackingAsync(id);

            if (application == null)
            {
                return NotFound();
            }

            var updatedApplication = application.ToApplicationFromUpdateDTO(dto);
            await _repository.UpdateAsync(updatedApplication);

            return Ok(updatedApplication.ToDTOFromApplication());
        }

        // admin
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var application = await _repository.GetByIdAsync(id);

            if (application == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(application);

            return NoContent();
        }
    }
}
