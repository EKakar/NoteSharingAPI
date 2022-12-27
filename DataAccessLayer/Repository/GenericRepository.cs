using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using NoteSharingAPI.Models;

namespace DataAccessLayer.Repository
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        public void Delete(T t)
        {
            using var c = new NoteDbContext();
            c.Remove(t);
            c.SaveChanges();
        }

        public T GetById(int id)
        {
            using var c = new NoteDbContext();
            return c.Set<T>().Find(id);
        }

        public List<T> GetList()
        {
            using var c = new NoteDbContext();
            return c.Set<T>().ToList();
        }

        public void Insert(T t)
        {
            using var c = new NoteDbContext();
            c.Add(t);
            c.SaveChanges();
        }

        public bool Login(string mail, string password)
        {
            using var c = new NoteDbContext();
            var user = c.Users.Where(u => u.Mail == mail && u.Password == password).FirstOrDefault();

            if (user != null)
            {
                return true;
            }

            else
            {
                return false;
            }

        }

        public void Update(T t)
        {
            using var c = new NoteDbContext();
            c.Update(t);
            c.SaveChanges();
        }
    }
}
