using BusinessLayer.Concrete;
using DataAccessLayer.EnityFramework;
using Microsoft.AspNetCore.Mvc;

namespace NoteSharingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        UserManager userManager = new UserManager(new EfUserDal());
        NoteManager noteManager = new NoteManager(new EfNoteDal());
        public static bool isLogin = false;

        [HttpGet]
        public IActionResult Login(string mail, string password)
        {
            var user = userManager.TGetList().FirstOrDefault(x => x.Mail == mail);

            if (userManager.Login(mail, password))
            {
                var notes = noteManager.TGetList().Where(x => x.UserId == user.UserId);
                isLogin = true;

                return Ok(notes);
            }

            else
            {
                return BadRequest();
            }
        }
    }
}
