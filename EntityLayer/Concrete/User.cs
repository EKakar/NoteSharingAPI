using System.ComponentModel.DataAnnotations;

namespace NoteSharingAPI.Models
{
    public enum SchoolLevel
    {
        Primary,
        HighSchool,
        University
    }

    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public SchoolLevel SchoolLevel { get; set; }
    }
}
