
namespace IMDbMovies.Domain
{
    public class WatchItem : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public bool IsWatched { get; set; }
    }
}
