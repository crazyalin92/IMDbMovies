using DataAccess.Repositories;
using IMDbMovies.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Contracts;
using Services.Mapping;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class UserWatchListService : IUserService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<WatchItem> userWatchlistRepo;
        private readonly ILogger<UserWatchListService> logger;
        public UserWatchListService(IRepository<User> userRepository,
            IRepository<WatchItem> userWatchlistRepo, ILogger<UserWatchListService> logger)
        {
            this.userRepository = userRepository;
            this.userWatchlistRepo = userWatchlistRepo;
            this.logger = logger;
        }

        public async Task<ServiceResult<int>> AddMovieToWatchList(int userId, int movieId)
        {
            try
            {
                var newWatchItem = new WatchItem
                {
                    MovieId = movieId,
                    UserId = userId,
                    IsWatched = false
                };

                var result = await userWatchlistRepo.CreateAsync(newWatchItem);
                return new ServiceResult<int> { Status = ServiceResultType.Success, Result = result };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during adding new movie to users watchlist: ");
                return new ServiceResult<int> { Status = ServiceResultType.Error, Message = ex.Message };
            }
        }

        public async Task<ServiceResult<int>> AddUser(UserDto user)
        {
            try
            {
                var result = await userRepository.CreateAsync(user.MapToDomain());
                return new ServiceResult<int> { Status = ServiceResultType.Success, Result = result };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during adding new user: ");
                return new ServiceResult<int> { Status = ServiceResultType.Error, Message = ex.Message };
            }
        }

        public async Task<ServiceResult<bool>> MarkMovieAsWatched(int Id)
        {
            try
            {
                var watchItem = await userWatchlistRepo.GetByIdAsync(Id);

                if (watchItem != null)
                {
                    watchItem.IsWatched = true;
                    var result = await userWatchlistRepo.UpdateAsync(watchItem);
                    return new ServiceResult<bool> { Status = ServiceResultType.Success, Result = result };
                }
                else
                {              
                    return new ServiceResult<bool> { Status = ServiceResultType.NotFound, Message = "Wathlist Item not found" };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during marking movie as watched: ");
                return new ServiceResult<bool> { Status = ServiceResultType.Error, Message = ex.Message };
            }
        }

        public async Task<ServiceResult<User>> GetUsersById(int userId)
        {
            try
            {
                var user = await userRepository.GetByIdAsync(userId);
                if (user != null)
                {
                    return new ServiceResult<User> { Result = user };
                }
                else
                {
                    return new ServiceResult<User> { Status = ServiceResultType.NotFound, Message = "User not found" };
                }
            }
            catch (Exception ex)
            {
                return new ServiceResult<User> { Status = ServiceResultType.Error, Message = ex.Message };
            }
        }

        public async Task<ServiceResult<List<WatchItemDto>>> GetWatchListForUser(int userId)
        {
            try
            {
                var watchList = await userWatchlistRepo
                    .Get(item => item.UserId == userId)
                    .Include(m => m.Movie).ToListAsync();

                if (watchList != null)
                {
                    var watchListMovies = watchList.Select(m => m.MapToDto()).ToList();

                    return new ServiceResult<List<WatchItemDto>> { Result = watchListMovies };
                }
                else
                {
                    return new ServiceResult<List<WatchItemDto>> { Status = ServiceResultType.NoContent, Message = "Watchlist for user not found" };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during getting watchlist for user: ");
                return new ServiceResult<List<WatchItemDto>> { Status = ServiceResultType.Error, Message = ex.Message };
            }
        }
    }
}
