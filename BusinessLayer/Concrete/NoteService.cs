using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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
            var note = _noteDbContext.Notes.Where(x => x.NoteId == id).FirstOrDefault();
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

        public async Task DeleteNote(int id)
        {
            await _noteDbContext.Database.ExecuteSqlRawAsync($"Exec sp_DeleteNote {id}");
            await _fileService.DeleteFile(id);
        }
    }
}
