using EntityLayer.Enums;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{

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
