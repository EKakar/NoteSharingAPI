using System.Diagnostics.CodeAnalysis;

namespace EntityLayer.Concrete
{
    public class Note
    {
        public int NoteId { get; set; }
        public string Description { get; set; }

        public int RatingScore { get; set; }
        public int NoteLevel { get; set; }

        public int UserId { get; set; }

        //[AllowNull]
        //public Category Category { get; set; }

        //[AllowNull]
        //public User User { get; set; }
    }
}