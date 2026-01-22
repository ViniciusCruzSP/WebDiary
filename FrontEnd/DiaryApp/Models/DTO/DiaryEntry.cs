namespace DiaryApp.Models.DTO
{
    public class DiaryEntry
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
    }
}
