using IMDbMovies.Domain;
using Services.Models;

namespace Services.Mapping
{
    public static class MovieMapper
    {
        public static Movie MapToDomain(this MovieDto movie)
        {
            return new Movie()
            {
                Id = movie.MovieId,
                ImdbId = movie.ImdbId,
                ImdbRating = movie.ImdbRating,
                PosterUrl = movie.PosterUrl,          
                Title = movie.Title,
                WikiDescription = movie.WikiDescription              
            };
        }

        public static MovieDto MapToDto(this Movie movie)
        {
            return new MovieDto()
            {
                MovieId = movie.Id,
                ImdbId = movie.ImdbId,
                ImdbRating = movie.ImdbRating,
                PosterUrl = movie.PosterUrl,
                Title = movie.Title,
                WikiDescription = movie.WikiDescription
            };
        }
    }
}
