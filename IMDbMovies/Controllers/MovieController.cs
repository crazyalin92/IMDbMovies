using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Services.Models;
using System.Threading.Tasks;

namespace IMDbMovies
{
    public class MovieController : ControllerBase
    {
        private readonly IMovieService movieService;

        private readonly IIMDbService imdbService;

        public MovieController(IMovieService movieService, IIMDbService iMDbService)
        {
            this.movieService = movieService;
            this.imdbService = iMDbService;
        }

        /// <summary>
        /// Add new movie
        /// </summary>
        [HttpGet]
        [Route("SaveMovie/{ImdbId}")]
        public async Task<ActionResult> SaveMovie(string ImdbId)
        {
            var result = await movieService.AddNewMovie(ImdbId);
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
        /// Search movie using IMDb service
        /// </summary>
        [HttpGet]
        [Route("SearchByTitle/{title}")]
        public async Task<ActionResult> SearchByTitle(string title)
        {
            var searchMovie = await imdbService.SearchByTitle(title);

            if (string.IsNullOrEmpty(searchMovie?.ErrorMessage))
            {
                return Ok(searchMovie.Results);
            }
            else
            {
                return BadRequest(searchMovie.ErrorMessage);
            }
        }

        /// <summary>
        /// Get movie wikipedia description 
        /// </summary>
        [HttpGet]
        [Route("GetWikipediaDescription/{IMDbId}")]
        public async Task<ActionResult> GetWikipediaDescription(string IMDbId)
        {
            var wikipediaData = await imdbService.GetWikipediaDescriptionById(IMDbId);

            if (string.IsNullOrEmpty(wikipediaData?.ErrorMessage))
            {
                return Ok(wikipediaData);
            }
            else
            {
                return BadRequest(wikipediaData.ErrorMessage);
            }
        }

        /// <summary>
        /// Get movie wikipedia description 
        /// </summary>
        [HttpGet]
        [Route("GetPosterById/{IMDbId}")]
        public async Task<ActionResult> GetPosterById(string IMDbId)
        {
            var wikipediaData = await imdbService.GetPosterById(IMDbId);

            if (string.IsNullOrEmpty(wikipediaData?.ErrorMessage))
            {
                return Ok(wikipediaData);
            }
            else
            {
                return BadRequest(wikipediaData.ErrorMessage);
            }
        }
    }
}
