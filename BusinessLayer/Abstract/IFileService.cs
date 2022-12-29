using EntityLayer.Enums;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Abstract
{
    public interface IFileService
    {
        Task PostFileAsync(IFormFile fileData, FileType fileType);
        Task DownloadFileById(int id);
    }
}
