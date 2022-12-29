using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly NoteDbContext _noteDbContext;

        public CategoryService(NoteDbContext noteDbContext)
        {
            _noteDbContext = noteDbContext;
        }

        public void TAdd(Category t)
        {
            _noteDbContext.Add(t);
            _noteDbContext.SaveChanges();
        }

        public void TDelete(Category t)
        {
            _noteDbContext.Remove(t);
            _noteDbContext.SaveChanges();
        }

        public Category TGetByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<Category> TGetList()
        {
            return _noteDbContext.Categories.ToList();
        }

        public void TUpdate(Category t)
        {
            _noteDbContext.Update(t);
            _noteDbContext.SaveChanges();
        }
    }
}
