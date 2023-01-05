using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NoteSharingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : Controller
    {
        private readonly INoteService _noteService;
        private readonly IUserService _userService;

        public NoteController(INoteService noteService, IUserService userService)
        {
            _noteService = noteService;
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllNotes()
        {
            var notes = _noteService.TGetList();
            return Ok(notes);
        }

        [Authorize]
        [HttpPost("addNote")]
        public async Task<IActionResult> AddNotes([FromBody] Note note)
        {
            _noteService.TAdd(note);
            return Ok(note);
        }

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetNoteForUser(int userId)
        {
            var user = _userService.TGetByID(userId);

            var notes = _noteService.TGetList().Where(x => x.UserId == user.UserId).ToList();
            return Ok(notes);
        }

        [Authorize]
        [HttpGet("findNote")]
        public async Task<IActionResult> GetNoteBySchoolLevel(string mail)
        {
            var user = _userService.TGetList().Where(u => u.Mail == mail).FirstOrDefault();
            var notes = _noteService.TGetList().Where(x => x.NoteLevel == user.SchoolLevel).ToList();
            return Ok(notes);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            _noteService.TDelete(_noteService.TGetByID(id));

            return Ok(new
            {
                Message = "Successfully Deleted!"
            });
        }
    }
}
