using BusinessLayer.Concrete;
using DataAccessLayer.EnityFramework;
using Microsoft.AspNetCore.Mvc;
using NoteSharingAPI.Models;

namespace NoteSharingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        UserManager userManager = new UserManager(new EfUserDal());


        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            userManager.TAdd(user);
            return View();
        }
    }
}
