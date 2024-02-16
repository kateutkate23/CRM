using API.DTO.Blog;
using CRM.Models;

namespace API.Mappers
{
    public static class BlogMapper
    {
        public static BlogDTO ToDTOFromBlog(this Blog blog)
        {
            BlogDTO dto = new()
            {
                Id = blog.Id,
                Title = blog.Title,
                ImageURL = blog.ImageURL,
                Description = blog.Description,
                CreatedDate = blog.CreatedDate
            };

            return dto;
        }

        public static Blog ToBlogFromCreateDTO(this CreateBlogDTO dto)
        {
            Blog blog = new()
            {
                Title = dto.Title,
                Description = dto.Description,
                CreatedDate = DateTime.Now
            };

            return blog;
        }

        public static Blog ToBlogFromUpdateDTO(this Blog blog, UpdateBlogDTO dto)
        {
            Blog result = new()
            {
                Id = blog.Id,
                Title = dto.Title ?? blog.Title,
                ImageURL = blog.ImageURL,
                Description = dto.Description ?? blog.Description,
                CreatedDate = blog.CreatedDate
            };

            return result;
        }
    }
}
