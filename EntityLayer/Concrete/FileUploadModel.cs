using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Concrete
{
    public class FileUploadModel
    {
        [NotMapped]
        public IFormFile FileDetails { get; set; }
    }
}
