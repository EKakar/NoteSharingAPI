using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using EntityLayer.Dto;
using EntityLayer.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NoteSharingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly NoteDbContext _noteDbContext;

        public UserController(IUserService userService, NoteDbContext noteDbContext)
        {
            _noteDbContext = noteDbContext;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            var users = _userService.TGetList();
            return Ok(users);
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
            var newAccessToken = user.Token;
            var newRefreshToken = _userService.CreateRefreshToken();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(1);
            await _noteDbContext.SaveChangesAsync();

            return Ok(new TokenApiDto()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(TokenApiDto tokenApiDto)
        {
            if (tokenApiDto is null)
                return BadRequest("Invalid Client Request");
            string accessToken = tokenApiDto.AccessToken;
            string refreshToken = tokenApiDto.RefreshToken;
            var principal = _userService.GetPrincipalFromExpiredToken(accessToken);
            var mail = principal.Identity.Name;
            var user = await _noteDbContext.Users.FirstOrDefaultAsync(u => u.Mail == mail);
            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Invalid Request");
            var newAccessToken = _userService.CreateJwtToken(user);
            var newRefreshToken = _userService.CreateRefreshToken();
            user.RefreshToken = newRefreshToken;
            await _noteDbContext.SaveChangesAsync();
            return Ok(new TokenApiDto()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }
    }
}
