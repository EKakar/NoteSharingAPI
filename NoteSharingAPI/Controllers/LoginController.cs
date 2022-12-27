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
        public static bool isLogin = false;

        [HttpGet]
        public IActionResult Login(string mail, string password)
        {
            var user = userManager.TGetList().FirstOrDefault(x => x.Mail == mail);

            if (userManager.Login(mail, password))
            {
                isLogin = true;
                
                return Ok(user);
            }

            else
            {
                return BadRequest();
            }
        }
    }
}
