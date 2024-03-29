﻿namespace MVC.ViewModels
{
    public class UpdateBlogVM
    {
        public int Id { get; set; } 
        public string? Title { get; set; } 
        public string? Description { get; set; } 
        public IFormFile? Image { get; set; }
        public string? ImageURL { get; set; }
    }
}
