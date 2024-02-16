using CRM.Models;

namespace API.Helpers
{
    public static class FileHelper
    {
        public static async Task<string?> SaveToImages(this IFormFile? file, Project project)
        {
            if (file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine("Resources", "Images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                project.ImageURL = filePath;

                return filePath;
            }

            return null;
        }
        public static void DeleteFile(string? path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
