using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface INoteService : IGenericService<Note>
    {
        IEnumerable<Note> TGetListByAction(Func<Note, bool> condition);
        IEnumerable<Note> TGetListBySchoolLevel(Func<Note, bool> condition);
        Task DeleteNote(int id);
    }
}
