using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using EntityLayer.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using static DataAccessLayer.Concrete.NoteDbContext;

namespace BusinessLayer.Concrete
{
    public class UserService : IUserService
    {
        private readonly NoteDbContext _noteDbContext;

        public UserService(NoteDbContext noteDbContext)
        {
            _noteDbContext = noteDbContext;
        }

        public async Task<bool> CheckEmailExistAsync(string email)
        {
            return await _noteDbContext.Users.AnyAsync(x => x.Mail == email);
        }

        public string CheckPasswordStrength(string password)
        {
            StringBuilder sb = new StringBuilder();
            if (password.Length < 8)
                sb.Append("Minimum password length should be 8 character" + Environment.NewLine);

            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[0-9]")))
                sb.Append("Password should contain upper and lower alphanumeric characters" + Environment.NewLine);

            if (!Regex.IsMatch(password, "[<,>,@,!,#,$,%,^,&,*,(,),_,+,\\[,\\], {,},?,:,;,|,',\\,../,~,`,=,-]"))
                sb.Append("Password should contain special characters" + Environment.NewLine);

            return sb.ToString();

        }

        public string CreateJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysceret.....");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Name, $"{user.FirstName}"),
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddSeconds(5),
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescripter);
            return jwtTokenHandler.WriteToken(token);
        }

        public string CreateRefreshToken()
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var refreshToken = Convert.ToBase64String(tokenBytes);

            var tokenInUser = _noteDbContext.Users.Any(a => a.RefreshToken == refreshToken);

            if (tokenInUser)
            {
                return CreateRefreshToken();
            }
            return refreshToken;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var key = Encoding.ASCII.GetBytes("veryverysceret.....");

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("This is Invalid Token");
            return principal;
        }


        public User FindUser(string mail)
        {
            var user = _noteDbContext.Users.FirstOrDefault(x => x.Mail == mail);
            return user;
        }

        public int GetUserId(string mail)
        {
            var userObj = _noteDbContext.Users.FirstOrDefault(x => x.Mail == mail);
            return userObj.UserId;
        }

        public void TAdd(User t)
        {
            t.Password = PasswordHasher.HashPassword(t.Password);
            t.Role = "User";
            t.Token = "";

            _noteDbContext.Add(t);
            _noteDbContext.SaveChanges();
        }

        public void TDelete(User t)
        {
            _noteDbContext.Remove(t);
            _noteDbContext.SaveChanges();

        }

        public User TGetByID(int id)
        {
            return _noteDbContext.Users.Where(x => x.UserId == id).FirstOrDefault();
        }

        public List<User> TGetList()
        {
            var users = _noteDbContext.Users.FromSqlInterpolated($"EXEC sp_GetUsers").ToList();

            return users;
        }

        public void TUpdate(User t)
        {
            _noteDbContext.Update(t);
            _noteDbContext.SaveChanges();
        }

    }
}
