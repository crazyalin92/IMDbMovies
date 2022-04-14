using DataAccess.Repositories;
using IMDbMovies.Domain;
using Microsoft.Extensions.Logging;
using Services.Contracts;
using Services.IMDBService.Models;
using Services.Mapping;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class MovieService : IMovieService
    {
        private readonly IRepository<Movie> movieRepository;
        private readonly IIMDbService imdbService;
        private readonly ILogger<MovieService> logger;
        public MovieService(IRepository<Movie> movieRepository, IIMDbService imdbService, ILogger<MovieService> logger)
        {
            this.movieRepository = movieRepository;
            this.imdbService = imdbService;
            this.logger = logger;
        }

        public async Task<ServiceResult<int>> AddNewMovie(MovieDto movie)
        {
            try
            {
                var newId = await movieRepository.CreateAsync(movie.MapToDomain());

                return new ServiceResult<int> { Result = newId, Status = ServiceResultType.Success };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during adding new movie: ");
                return new ServiceResult<int> { Status = ServiceResultType.Error, Message = ex.Message };
            }
        }

        public async Task<ServiceResult<MovieDto>> GetMovieById(int Id)
        {
            try
            {
                var movie = await movieRepository.GetByIdAsync(Id);
                if (movie != null)
                {
                    return new ServiceResult<MovieDto>() { Result = movie.MapToDto() };
                }
                else
                {
                    return new ServiceResult<MovieDto>() { Status = ServiceResultType.NotFound, Message = "No data" };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during getting movie: ");
                return new ServiceResult<MovieDto>() { Status = ServiceResultType.Error, Message = ex.Message };
            }
        }

        public async Task<ServiceResult<bool>> UpdateMovie(MovieDto movie)
        {
            try
            {
                var result = await movieRepository.UpdateAsync(movie.MapToDomain());

                return new ServiceResult<bool>() { Result = result };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during updating movie: ");
                return new ServiceResult<bool>() { Result = false, Status = ServiceResultType.Error, Message = ex.Message };
            }
        }

        public async Task<ServiceResult<int>> AddNewMovie(string imdbId)
        {
            try
            {
                bool isMovieExist = await movieRepository.AnyAsync(m => m.ImdbId == imdbId);
                if (isMovieExist)
                {
                    return new ServiceResult<int> { Message = "Movie already exist in local db", Result = 1 };
                }

                TitleData titleData = await imdbService.GetInfoById(imdbId, new List<string> { "Wikipedia", "Posters", "Ratings" });
                if (!string.IsNullOrEmpty(titleData?.ErrorMessage))
                {
                    return new ServiceResult<int>() { Message = titleData.ErrorMessage };
                }

                var newMovie = new Movie()
                {
                    ImdbId = imdbId,
                    ImdbRating = titleData.IMDbRating,
                    PosterUrl = titleData.Posters.Posters[0].Link,
                    Title = titleData.Title,
                    WikiDescription = titleData.Wikipedia.PlotShort.PlainText
                };

                var newId = await movieRepository.CreateAsync(newMovie);

                return new ServiceResult<int>() { Status = ServiceResultType.Success, Result = newId };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error during adding new movie: ");
                return new ServiceResult<int>() { Status = ServiceResultType.Error, Message = ex.Message };
            }
        }
    }
}
