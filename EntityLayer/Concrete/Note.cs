namespace EntityLayer.Concrete
{
    public class Note
    {
        public int NoteId { get; set; }
        public string Description { get; set; }
        public int RatingScore { get; set; }
        public int NoteLevel { get; set; }
        public int UserId { get; set; }
    }
}