using IMDbMovies.Domain;
using Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IMovieService
    {
        Task<ServiceResult<MovieDto>> GetMovieById(int Id);
        Task<ServiceResult<int>> AddNewMovie(MovieDto movie);
        Task<ServiceResult<int>> AddNewMovie(string imdbId);
        Task<ServiceResult<bool>> UpdateMovie(MovieDto movie);
    }
}
