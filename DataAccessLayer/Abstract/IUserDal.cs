using NoteSharingAPI.Models;

namespace DataAccessLayer.Abstract
{
    public interface IUserDal : IGenericDal<User>
    {
        bool Login(string mail, string password);
    }
}
