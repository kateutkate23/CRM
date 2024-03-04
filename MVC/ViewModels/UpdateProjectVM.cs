namespace MVC.ViewModels
{
    public class UpdateProjectVM
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? ImageURL { get; set; }
        public IFormFile? Image { get; set; }
        public string? Description { get; set; }
    }
}
