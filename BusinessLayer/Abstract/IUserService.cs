using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IUserService : IGenericService<User>
    {
        bool Login(string mail, string password);
    }
}
