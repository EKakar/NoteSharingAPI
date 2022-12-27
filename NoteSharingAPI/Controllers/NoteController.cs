using BusinessLayer.Concrete;
using DataAccessLayer.EnityFramework;
using Microsoft.AspNetCore.Mvc;
using NoteSharingAPI.Models;

namespace NoteSharingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : Controller
    {
        NoteManager noteManager = new NoteManager(new EfNoteDal());
        UserManager userManager = new UserManager(new EfUserDal());

        [HttpGet]
        public IActionResult GetAllNotes()
        {
            var notes = noteManager.TGetList();
            return Ok(notes);
        }

        [HttpPost]
        public async Task<IActionResult> AddNotes([FromBody] Note note)
        {
            noteManager.TAdd(note);
            return Ok(note);
        }

        [HttpGet("/{userId}")]
        public async Task<IActionResult> GetNoteForUser(int userId)
        {
            var user = userManager.TGetList().FirstOrDefault(x => x.UserId == userId);

            var notes = noteManager.TGetList().Where(x => x.UserId == user.UserId).ToList();
            return Ok(notes);
        }

        [HttpGet("{mail}")]
        public async Task<IActionResult> GetNoteBySchoolLevel(string mail)
        {
            var user = userManager.TGetList().Where(u => u.Mail == mail).FirstOrDefault();
            var notes = noteManager.TGetList().Where(x => x.NoteLevel == user.SchoolLevel).ToList();
            return Ok(notes);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            var note = noteManager.TGetByID(id);
            noteManager.TDelete(note);

            return RedirectToAction("GetAllNotes");
        }
    }
}
