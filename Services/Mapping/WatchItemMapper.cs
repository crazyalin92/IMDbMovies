using IMDbMovies.Domain;
using Services.Models;

namespace Services.Mapping
{
    public static class WatchItemMapper
    {
        public static WatchItem MapToDomain(this WatchItemDto watchItem)
        {
            return new WatchItem
            {
                Id = watchItem.WatchItemId,
                MovieId = watchItem.MovieId,
                UserId = watchItem.UserId,
                IsWatched = watchItem.IsWatched,
            };
        }

        public static WatchItemDto MapToDto(this WatchItem watchItem)
        {
            return new WatchItemDto
            {
                WatchItemId = watchItem.Id,
                MovieId = watchItem.MovieId,
                UserId = watchItem.UserId,
                ImdbId = watchItem.Movie?.ImdbId,
                Title = watchItem.Movie?.Title,
                WikiDescription = watchItem.Movie?.WikiDescription,
                ImdbRating = watchItem.Movie?.ImdbRating,
                PosterUrl = watchItem.Movie?.PosterUrl
            };
        }
    }
}
