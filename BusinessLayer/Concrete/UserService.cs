using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class UserService : IUserService
    {
        private readonly NoteDbContext _noteDbContext;

        public UserService(NoteDbContext noteDbContext)
        {
            _noteDbContext = noteDbContext;
        }

        public bool Login(string mail, string password)
        {
            throw new NotImplementedException();
        }

        public void TAdd(User t)
        {
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
            return _noteDbContext.Users.ToList();
        }

        public void TUpdate(User t)
        {
            _noteDbContext.Update(t);
            _noteDbContext.SaveChanges();
        }
    }
}
