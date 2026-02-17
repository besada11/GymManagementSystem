using Microsoft.AspNetCore.Http;

namespace GymManagementBLL.Services.AttachmentService
{
    public interface IAttachmentService
    {
        //Upload file
        public string? UploadFile(string folderName, IFormFile file);
        //Delete file
        public bool Delete(string folderName, string fileName);
    }
}