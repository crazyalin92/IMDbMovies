using IMDbMovies.Domain;
using Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IUserService
    {
        Task<ServiceResult<List<WatchItemDto>>> GetWatchListForUser(int userId);
        Task<ServiceResult<User>> GetUsersById(int userId);
        Task<ServiceResult<int>> AddUser(UserDto user);
        Task<ServiceResult<bool>> MarkMovieAsWatched(int watchItemId);
        Task<ServiceResult<int>> AddMovieToWatchList(int userId, int movieId);
    }
}
