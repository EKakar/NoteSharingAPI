using EntityLayer.Enums;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Abstract
{
    public interface IFileService
    {
        Task PostFileAsync(IFormFile fileData);
        Task DownloadFileById(int id);
        void DeleteFile(int id);
    }
}
