
namespace IMDbMovies.Domain
{
    public class Movie: BaseEntity
    {
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public string WikiDescription { get; set; }
        public string ImdbRating { get; set; }
        public string PosterUrl { get; set; }
    }
}
