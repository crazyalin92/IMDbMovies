using Services.IMDBService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IIMDbService
    {
        Task<SearchData> SearchByTitle(string title);
        Task<TitleData> GetInfoById(string IMDbId, List<string> options);
        Task<PosterData> GetPosterById(string titleId);
        Task<WikipediaData> GetWikipediaDescriptionById(string IMDbId);
    }
}
