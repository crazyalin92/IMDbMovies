using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Services.Contracts;
using Services.IMDBService;
using Services.IMDBService.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services
{
    public class IMDbService : IIMDbService
    {
        private readonly ImdSettings settings;

        private readonly ILogger<IMDbService> logger;

        private HttpClient _httpClient;
        private HttpClient HttpClient
        {
            get
            {
                if (_httpClient == null)
                {
                    _httpClient = new HttpClient();
                }
                return _httpClient;
            }
        }

        public IMDbService(IOptions<ImdSettings> settings, ILogger<IMDbService> logger)
        {
            this.settings = settings.Value;
            this.logger = logger;
        }

        public async Task<TitleData> GetInfoById(string IMDbId, List<string> options)
        {
            try
            {
                var path = settings.GetTitleByIdUrl(IMDbId, options);

                var response = await HttpClient.GetAsync(path);

                if (!response.IsSuccessStatusCode)
                {
                    return new TitleData { ErrorMessage = "Errors with http request to Imdb Api" };
                }

                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TitleData>(responseBody);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "IMDbService.GetInfoById");

                return new TitleData
                {
                    ErrorMessage = "Error in GetInfoById."
                };
            }
        }

        public async Task<PosterData> GetPosterById(string titleId)
        {
            try
            {
                var path = settings.GetPosterByIdUrl(titleId);

                var response = await HttpClient.GetAsync(path);

                if (!response.IsSuccessStatusCode)
                {
                    return new PosterData { ErrorMessage = "Errors with http request to Imdb Api" };
                }

                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<PosterData>(responseBody);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "IMDbService.GetPosterById");

                return new PosterData
                {
                    ErrorMessage = "Error in GetInfoById."
                };
            }
        }

        public async Task<WikipediaData> GetWikipediaDescriptionById(string IMDbId)
        {
            try
            {
                var path = settings.GetWikipediaDescriptionByIdUrl(IMDbId);

                var response = await HttpClient.GetAsync(path);

                if (!response.IsSuccessStatusCode)
                {
                    return new WikipediaData { ErrorMessage = "Errors with http request to Imdb Api(GetWikipediaDescriptionById)" };
                }

                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<WikipediaData>(responseBody);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "IMDbService.GetWikipediaDescriptionById");

                return new WikipediaData
                {
                    ErrorMessage = "Error in GetWikipediaDescriptionById."
                };
            }
        }

        public async Task<SearchData> SearchByTitle(string title)
        {
            try
            {
                var path = settings.GetSearchByTitleUrl(title);

                var response = await HttpClient.GetAsync(path);

                if (!response.IsSuccessStatusCode)
                {
                    return new SearchData { ErrorMessage = "Errors with http request to Imdb Api(SearchByTitle)" };
                }

                string responseBody = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<SearchData>(responseBody);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "IMDbService.SearchByTitle");

                return new SearchData
                {
                    ErrorMessage = "Error in SearchByTitle."
                };
            }
        }
    }
}
