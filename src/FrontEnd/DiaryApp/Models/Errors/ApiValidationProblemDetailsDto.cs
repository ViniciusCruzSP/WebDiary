namespace DiaryApp.Models.Errors
{
    public class ApiValidationProblemDetailsDto
    {
        public string? Title { get; set; }
        public int Status { get; set; }
        public string? Detail { get; set; }
        public Dictionary<string, string[]> Errors { get; set; } = new();
    }
}
