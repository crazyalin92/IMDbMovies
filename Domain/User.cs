
using System.Collections.Generic;

namespace IMDbMovies.Domain
{
    public class User: BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        List<WatchItem> Watchlist { get; set; }
    }
}
