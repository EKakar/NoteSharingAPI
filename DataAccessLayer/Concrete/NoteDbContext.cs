using Microsoft.EntityFrameworkCore;
using NoteSharingAPI.Models;

namespace DataAccessLayer.Concrete
{
    public class NoteDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = E-KAKAR; Database = NoteSharingDB; Uid = sa; pwd = 1234;");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
