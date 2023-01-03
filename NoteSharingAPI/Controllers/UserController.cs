using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace NoteSharingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();

            if (await _userService.CheckEmailExistAsync(userObj.Mail))
                return BadRequest(new { Message = "Email Already Exist!" });

            var pass = _userService.CheckPasswordStrength(userObj.Password);
            if (!string.IsNullOrEmpty(pass))
                return BadRequest(new { Message = pass.ToString() });


            _userService.TAdd(userObj);
            return Ok(new
            {
                Message = "User Registered!"
            });
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User userObj)
        {
            if (userObj == null)
            {
                return BadRequest();
            }

            var user = _userService.FindUser(userObj.Mail);

            if (user == null)
                return NotFound(new { Message = "User Not Found!" });

            if (!PasswordHasher.VerifyPassword(userObj.Password, user.Password))
            {
                return BadRequest(new { Message = "Password is Incorrect!" });
            }

            user.Token = _userService.CreateJwtToken(user);

            return Ok(new
            {
                Token = user.Token,
                Message = "Login Success!"
            });

        }
    }
}
