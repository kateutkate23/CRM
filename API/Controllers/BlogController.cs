using API.DTO.Blog;
using API.Helpers;
using API.Interfaces;
using API.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/blog")]
    [ApiController]
    public class BlogController(IBlogRepository repository) : ControllerBase
    {
        private readonly IBlogRepository _repository = repository;

        // guest, admin
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var blogs = await _repository.GetAllAsync();
            var blogsDTO = blogs.Select(b => b.ToDTOFromBlog());

            return Ok(blogsDTO);
        }

        // guest, admin
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var blog = await _repository.GetByIdAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            return Ok(blog.ToDTOFromBlog());
        }

        // admin
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateBlogDTO dto, IFormFile? img)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (img == null)
            {
                ModelState.AddModelError("img", "The image field is required");
                return BadRequest(ModelState);
            }

            var blog = dto.ToBlogFromCreateDTO();

            await img.SaveToImages(blog);
            await _repository.CreateAsync(blog);

            return CreatedAtAction(nameof(GetById), new { id = blog.Id }, blog.ToDTOFromBlog());
        }

        // admin
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromForm] UpdateBlogDTO dto, IFormFile? img)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var blog = await _repository.GetByIdNoTrackingAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            var updatedBlog = blog.ToBlogFromUpdateDTO(dto);

            if (img != null)
            {
                await img.SaveToImages(updatedBlog);
            }

            await _repository.UpdateAsync(updatedBlog);

            return Ok(updatedBlog.ToDTOFromBlog());
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

            var blog = await _repository.GetByIdAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(blog);
            FileHelper.DeleteFile(blog.ImageURL);

            return NoContent();
        }
    }
}
