namespace NoteSharingAPI.Models
{
    public class Note
    {
        public int NoteId { get; set; }
        public string Description { get; set; }
        public string FileUrl { get; set; }
        public int RatingScore { get; set; }
        public SchoolLevel NoteLevel { get; set; }

        public int CategoryID { get; set; }
        public Category? Category { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}