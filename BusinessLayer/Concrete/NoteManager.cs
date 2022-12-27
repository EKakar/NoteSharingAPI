using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using NoteSharingAPI.Models;

namespace BusinessLayer.Concrete
{
    public class NoteManager : INoteService
    {
        private readonly INoteDal _noteDal;

        public NoteManager(INoteDal noteDal)
        {
            _noteDal = noteDal;
        }

        public void TAdd(Note t)
        {
            _noteDal.Insert(t);
        }

        public void TDelete(Note t)
        {
            _noteDal.Delete(t);
        }

        public Note TGetByID(int id)
        {
            return _noteDal.GetById(id);
        }

        public List<Note> TGetList()
        {
            return _noteDal.GetList();
        }

        public void TUpdate(Note t)
        {
            _noteDal.Update(t);
        }
    }
}
