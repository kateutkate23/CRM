using API.DTO.Service;
using API.Interfaces;
using API.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/service")]
    [ApiController]
    public class ServiceController(IServiceRepository repository) : ControllerBase
    {
        private readonly IServiceRepository _repository = repository;

        // guest, admin
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var services = await _repository.GetAllAsync();
            var servicesDTO = services.Select(s => s.ToDTOFromService());

            return Ok(servicesDTO);
        }

        // guest(?), admin
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = await _repository.GetByIdAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return Ok(service.ToDTOFromService());
        }

        // admin
        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateServiceDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var service = dto.ToServiceFromCreateDTO();
            await _repository.CreateAsync(service);

            return CreatedAtAction(nameof(GetById), new { id = service.Id }, service.ToDTOFromService());
        }

        // admin
        [HttpPut]
        [Route("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateServiceDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            var service = await _repository.GetByIdNoTrackingAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            var updatedService = service.ToServiceFromUpdateDTO(dto);
            await _repository.UpdateAsync(updatedService);

            return Ok(updatedService.ToDTOFromService());
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

            var service = await _repository.GetByIdAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(service);

            return NoContent();
        }
    }
}
