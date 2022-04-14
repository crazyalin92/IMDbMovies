using System.Collections.Generic;

namespace Services.IMDBService
{
    public class ImdSettings
    {
        private const string SearchByTitle = "{0}/Search/{1}/{2}";
        private const string GetTitleById = "{0}/Title/{1}/{2}/{3}";
        private const string GetPosterById = "{0}/Posters/{1}/{2}";
        private const string GetWikipediaDescriptionById = "{0}/Wikipedia/{1}/{2}";

        public string ApiKey { get; set; }
        public string BaseImdbUrl { get; set; }

        public string GetSearchByTitleUrl(string title)
        {
            return string.Format(SearchByTitle, BaseImdbUrl, ApiKey, title);
        }

        public string GetTitleByIdUrl(string id, List<string> options)
        {
            string expression = string.Join(",", options);

            return string.Format(GetTitleById, BaseImdbUrl, ApiKey, id, expression);
        }

        public string GetPosterByIdUrl(string id)
        {
            return string.Format(GetPosterById, BaseImdbUrl, ApiKey, id);
        }

        public string GetWikipediaDescriptionByIdUrl(string id)
        {
            return string.Format(GetWikipediaDescriptionById, BaseImdbUrl, ApiKey, id);
        }
    }
}
