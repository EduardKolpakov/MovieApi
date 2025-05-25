namespace MovieApi.Model
{
    public class MovieChatMessage
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public int MovieId { get; set; }
    }
}
