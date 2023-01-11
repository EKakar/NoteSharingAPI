using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace NoteSharingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _iFileService;

        public FilesController(IFileService fileService)
        {
            _iFileService = fileService;
        }

        /// <summary>
        /// Single File Upload
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("PostFile")]
        public async Task<ActionResult> PostSingleFile()
        {

            try
            {
                var formCollection = await Request.ReadFormAsync();

                var file = formCollection.Files[0];

                await _iFileService.PostFileAsync(file);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Download File
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpGet("DownloadFile/{id}")]
        public async Task<ActionResult> DownloadFile(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            try
            {
                await _iFileService.DownloadFileById(id);
                return Ok(new
                {
                    Message = "File is Successfully Downloaded!"
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
