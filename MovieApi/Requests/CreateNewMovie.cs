namespace MovieApi.Requests
{
    public class CreateNewMovie
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public DateOnly PublishingDate { get; set; }
        public double Rating { get; set; }
    }
}
