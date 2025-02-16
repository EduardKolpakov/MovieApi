using System.ComponentModel.DataAnnotations;

namespace MovieApi.Model
{
    public class Movies
    {
        [Key]
        public int ID_Movie { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public DateOnly PublishingDate { get; set; }
        public double Rating { get; set; }
    }
}
