using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class NoteService : INoteService
    {
        private readonly NoteDbContext _noteDbContext;

        public NoteService(NoteDbContext noteDbContext)
        {
            _noteDbContext = noteDbContext;
        }

        public void TAdd(Note t)
        {
            _noteDbContext.Add(t);
            _noteDbContext.SaveChanges();
        }

        public void TDelete(Note t)
        {
            _noteDbContext.Remove(t);
            _noteDbContext.SaveChanges();
        }

        public Note TGetByID(int id)
        {
            return _noteDbContext.Notes.Where(x => x.NoteId == id).FirstOrDefault();
        }

        public List<Note> TGetList()
        {
            return _noteDbContext.Notes.ToList();
        }

        public void TUpdate(Note t)
        {
            _noteDbContext.Update(t);
            _noteDbContext.SaveChanges();
        }
    }
}
