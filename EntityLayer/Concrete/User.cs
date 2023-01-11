using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{

    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public int SchoolLevel { get; set; }

        public string Token { get; set; }
        public string Role { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }


        public ICollection<Note> Notes { get; set; }

    }
}
