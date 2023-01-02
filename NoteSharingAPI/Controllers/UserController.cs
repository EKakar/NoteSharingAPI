using BusinessLayer.Abstract;
using EntityLayer.Concrete;
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


        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User userObj)
        {
            if (userObj == null)
            {
                return BadRequest();
            }

            var user = _userService.TGetList().FirstOrDefault(x => x.Mail == userObj.Mail && x.Password == userObj.Password);

            if (user == null)
                return NotFound(new { Message = "User Not Found!" });

            return Ok(new
            {
                Message = "Login Success!"
            });

        }


        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();


            _userService.TAdd(userObj);
            return Ok(new
            {
                Message = "User Registered!"
            });
        }
    }
}
