using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BusinessLayer.Abstract
{
    public interface IUserService : IGenericService<User>
    {
        bool Login(string mail, string password);
    }
}
