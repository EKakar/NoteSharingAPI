using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace NoteSharingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : Controller
    {
        private readonly IFileService _fileService;
        private readonly INoteService _noteService;
        private readonly IUserService _userService;

        public NoteController(IFileService fileService, INoteService noteService, IUserService userService)
        {
            _fileService = fileService;
            _noteService = noteService;
            _userService = userService;
        }


        /// <summary>
        /// Single File Upload
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("PostSingleFile")]
        public async Task<ActionResult> PostSingleFile([FromForm] FileUploadModel fileUploadModel)
        {
            if (fileUploadModel == null)
            {
                return BadRequest();
            }

            try
            {
                await _fileService.PostFileAsync(fileUploadModel.FileDetails, fileUploadModel.FileType);
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
                await _fileService.DownloadFileById(id);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }



        [HttpGet]
        public IActionResult GetAllNotes()
        {
            var notes = _noteService.TGetList();
            return Ok(notes);
        }

        [HttpPost]
        public async Task<IActionResult> AddNotes([FromBody] Note note)
        {
            _noteService.TAdd(note);
            return Ok(note);
        }

        [HttpGet("/{userId}")]
        public async Task<IActionResult> GetNoteForUser(int userId)
        {
            var user = _userService.TGetByID(userId);

            var notes = _noteService.TGetList().Where(x => x.UserId == user.UserId).ToList();
            return Ok(notes);
        }

        [HttpGet("{mail}")]
        public async Task<IActionResult> GetNoteBySchoolLevel(string mail)
        {
            var user = _userService.TGetList().Where(u => u.Mail == mail).FirstOrDefault();
            var notes = _noteService.TGetList().Where(x => x.NoteLevel == user.SchoolLevel).ToList();
            return Ok(notes);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            _noteService.TDelete(_noteService.TGetByID(id));

            return RedirectToAction("GetAllNotes");
        }
    }
}
