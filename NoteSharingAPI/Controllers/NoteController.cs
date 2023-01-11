using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
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

        public NoteController(INoteService noteService, IUserService userService, NoteDbContext noteDbContext)
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

        [HttpPost("addNote")]
        public async Task<IActionResult> AddNotes([FromBody] Note note)
        {

            _noteService.TAdd(note);
            return Ok(new
            {
                Message = "Successfully Added!"
            });
        }


        [Authorize]
        [HttpGet("{mail}")]
        public async Task<IActionResult> GetUserNotes(string mail)
        {
            var user = _userService.FindUser(mail);
            var notes = _noteService.TGetListByAction(x => x.UserId == user.UserId);
            if (notes == null)
            {
                return Ok(new List<Note>());
            }
            return Ok(notes);
        }

        [Authorize]
        [HttpGet("findNote")]
        public async Task<IActionResult> GetNoteBySchoolLevel(string mail)
        {
            var user = _userService.FindUser(mail);
            var notes = _noteService.TGetListBySchoolLevel(x => x.NoteLevel == user.SchoolLevel);
            return Ok(notes);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var note = _noteService.TGetByID(id);
            if (note == null)
            {
                return BadRequest("This file is already deleted");
            }

            await _noteService.DeleteNote(id);

            return Ok(new
            {
                Message = "Successfully Deleted!"
            });
        }
    }
}
