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



        [HttpGet]
        public IActionResult GetAllNotes()
        {
            var notes = _noteService.TGetList();
            return Ok(notes);
        }

        [HttpPost("addNote")]
        public async Task<IActionResult> AddNotes([FromBody] Note note)
        {
            _noteService.TAdd(note);
            return Ok(note);
        }

        [HttpGet("userNotes")]
        public async Task<IActionResult> GetNoteForUser(int userId)
        {
            var user = _userService.TGetByID(userId);

            var notes = _noteService.TGetList().Where(x => x.User.UserId == user.UserId).ToList();
            return Ok(notes);
        }

        [HttpGet("findNote")]
        public async Task<IActionResult> GetNoteBySchoolLevel(string mail)
        {
            var user = _userService.TGetList().Where(u => u.Mail == mail).FirstOrDefault();
            var notes = _noteService.TGetList().Where(x => x.NoteLevel == user.SchoolLevel).ToList();
            return Ok(notes);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            _noteService.TDelete(_noteService.TGetByID(id));

            return RedirectToAction("GetAllNotes");
        }
    }
}
