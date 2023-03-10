using EntityLayer.Concrete;
using System.Security.Claims;

namespace BusinessLayer.Abstract
{
    public interface IUserService : IGenericService<User>
    {
        Task<bool> CheckEmailExistAsync(string email);
        string CheckPasswordStrength(string password);
        User FindUser(string mail);
        string CreateJwtToken(User user);
        int GetUserId(string mail);
        string CreateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
