using EntityLayer.Concrete;
using static DataAccessLayer.Concrete.NoteDbContext;

namespace BusinessLayer.Abstract
{
    public interface INoteService : IGenericService<Note>
    {
        IEnumerable<Note> TGetListByAction(Func<Note, bool> condition);
        IEnumerable<Note> TGetListBySchoolLevel(Func<Note, bool> condition);
        void DeleteNote(int id);
        NoteCategory GetNoteAndCategory(int id);
    }
}

