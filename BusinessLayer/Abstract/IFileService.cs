using EntityLayer.Enums;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Abstract
{
    public interface IFileService
    {
        Task PostFileAsync(IFormFile fileData);
        Task DownloadFileById(int id);
        Task DeleteFile(int id);
    }
}
