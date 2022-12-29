using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace NoteSharingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly INoteService _noteService;
        private readonly IUserService _userService;

        public static bool isLogin = false;

        public LoginController(INoteService noteService, IUserService userService)
        {
            _noteService = noteService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login(string mail, string password)
        {
            var user = _userService.TGetList().FirstOrDefault(x => x.Mail == mail);

            if (_userService.Login(mail, password))
            {
                var notes = _noteService.TGetByID(user.UserId);
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
