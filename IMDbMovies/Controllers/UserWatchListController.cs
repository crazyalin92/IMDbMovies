using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.Models;
using System.Threading.Tasks;

namespace IMDbMovies
{
    public class UserWatchListController : ControllerBase
    {
        private readonly IUserWatchListService userService;

        public UserWatchListController(IUserWatchListService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Get watchlist for user
        /// </summary>
        [HttpGet]
        [Route("GetWatchListByUserId/{userId}")]
        public async Task<ActionResult> GetWatchListByUserId(int userId)
        {
            var result = await userService.GetWatchListForUser(userId);

            if (result.IsSuccess)
            {
                return Ok(result.Result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        /// <summary>
        /// Add new item to watchlist for user
        /// </summary>
        [HttpPost]
        [Route("AddToWatchList")]
        public async Task<ActionResult> AddMovieToWatchList(int userId, int movieId)
        {
            var result = await userService.AddMovieToWatchList(userId, movieId);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        /// <summary>
        /// Mark movie as watched
        /// </summary>
        [HttpPost]
        [Route("MarkMovieAsWatched/{watchItemId}")]
        public async Task<ActionResult> MarkMovieAsWatched(int watchItemId)
        {
            var result = await userService.MarkMovieAsWatched(watchItemId);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        // <summary>
        /// Add new user
        /// </summary>
        [HttpPost]
        [Route("AddUser")]
        public async Task<ActionResult> AddUser(UserDto user)
        {
            var result = await userService.AddUser(user);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
