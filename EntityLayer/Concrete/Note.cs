using EntityLayer.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Concrete
{
    public class Note
    {
        public int NoteId { get; set; }
        public string Description { get; set; }

        [NotMapped]
        public FileUploadModel FileUploadModel { get; set; }
        public int RatingScore { get; set; }
        public int NoteLevel { get; set; }

        public int CategoryID { get; set; }
        public Category? Category { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}