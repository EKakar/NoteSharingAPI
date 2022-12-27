using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using NoteSharingAPI.Models;

namespace BusinessLayer.Concrete
{
    public class UserManager : IGenericService<User>
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public bool Login(string mail, string password)
        {
            return _userDal.Login(mail, password);
        }

        public void TAdd(User t)
        {
            _userDal.Insert(t);
        }

        public void TDelete(User t)
        {
            _userDal.Delete(t);
        }

        public User TGetByID(int id)
        {
            return _userDal.GetById(id);
        }


        public List<User> TGetList()
        {
            return _userDal.GetList();
        }

        public void TUpdate(User t)
        {
            _userDal.Update(t);
        }
    }
}
