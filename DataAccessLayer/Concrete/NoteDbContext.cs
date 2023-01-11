using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Concrete
{

    public class NoteDbContext : DbContext
    {

        [NotMapped]
        public class CategoryList
        {
            public string Category { get; set; }
        }

        [NotMapped]
        public class NoteCategory
        {
            public string NoteDescription { get; set; }
            public string Category { get; set; }
        }


        public NoteDbContext(DbContextOptions<NoteDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryList>().HasNoKey();
            modelBuilder.Entity<NoteCategory>().HasNoKey();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FileDetails> FileDetails { get; set; }
        public DbSet<CategoryList> CategoryLists { get; set; }
        public DbSet<NoteCategory> NoteCategories { get; set; }

    }
}
