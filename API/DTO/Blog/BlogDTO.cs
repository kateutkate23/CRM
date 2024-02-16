namespace API.DTO.Blog
{
    public class BlogDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? ImageURL { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; } 
    }
}
