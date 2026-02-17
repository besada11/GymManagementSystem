using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace GymManagementBLL.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {

        public AttachmentService(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }
        private readonly IWebHostEnvironment _webHost;
        private readonly string[] allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        private readonly long maxFileSize = 5 * 1024 * 1024; // 5 MB

        public string? UploadFile(string folderName, IFormFile file)
        {
            try
            {
                if (folderName is null || file is null || file.Length == 0)
                    return null;

                if (file.Length > maxFileSize)
                    return null;

                var extension = Path.GetExtension(file.FileName).ToLower();
                if (!allowedExtensions.Contains(extension))
                    return null;

                var folderPath = Path.Combine(_webHost.WebRootPath, folderName);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var fileName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(folderPath, fileName);

                using var stream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(stream);

                return fileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to upload file = {folderName} : {ex.Message}");
                return null;
            }

        }

        public bool Delete(string folderName, string fileName)
        {
            try
            {
                if (string.IsNullOrEmpty(folderName) || string.IsNullOrEmpty(fileName))
                    return false;   

                var filePath = Path.Combine(_webHost.WebRootPath, folderName, fileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete file = {fileName} in folder = {folderName} : {ex.Message}");
                return false;
            }
        }
    }
}

