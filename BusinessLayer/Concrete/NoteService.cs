using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using static DataAccessLayer.Concrete.NoteDbContext;

namespace BusinessLayer.Concrete
{
    public class NoteService : INoteService
    {
        private readonly NoteDbContext _noteDbContext;
        private readonly IFileService _fileService;

        public NoteService(NoteDbContext noteDbContext, IFileService fileService)
        {
            _noteDbContext = noteDbContext;
            _fileService = fileService;
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
            var note = _noteDbContext.Notes.FirstOrDefault(x => x.NoteId == id);
            return note;
        }

        public List<Note> TGetList()
        {
            return _noteDbContext.Notes.ToList();
        }

        public IEnumerable<Note> TGetListByAction(Func<Note, bool> condition)
        {
            return _noteDbContext.Notes.Where(condition);
        }

        public IEnumerable<Note> TGetListBySchoolLevel(Func<Note, bool> condition)
        {
            return _noteDbContext.Notes.Where(condition);
        }

        public void TUpdate(Note t)
        {
            _noteDbContext.Update(t);
            _noteDbContext.SaveChanges();
        }

        public void DeleteNote(int id)
        {
            _noteDbContext.Database.ExecuteSqlRawAsync($"EXECUTE dbo.sp_deleteNote {id}");
            _fileService.DeleteFile(id);
            _noteDbContext.SaveChanges();
        }

        public NoteCategory? GetNoteAndCategory(int id)
        {
            var note = _noteDbContext.NoteCategories.FromSqlRaw($"exec sp_getNoteById {id}").ToList().FirstOrDefault();
            return note;
        }
    }
}
