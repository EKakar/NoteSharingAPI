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

        [HttpGet]
        [Authorize]
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
            return Ok(new
            {
                Message = "Successfully Added!"
            });
        }

        [Authorize]
        [HttpGet("get{id}")]
        public async Task<IActionResult> GetNoteForEdit(int id)
        {
            var note = _noteService.GetNoteAndCategory(id);

            return Ok(note);
        }

        [Authorize]
        [HttpGet("userNotes{mail}")]
        public async Task<IActionResult> GetUserNotes(string mail)
        {
            var user = _userService.TGetList().FirstOrDefault(x => x.Mail == mail);
            var notes = _noteService.TGetListByAction(x => x.UserId == user.UserId);
            if (notes == null)
            {
                return Ok(new List<Note>());
            }
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

            _noteService.DeleteNote(id);

            return Ok(new
            {
                Message = "Successfully Deleted!"
            });
        }
    }
}
