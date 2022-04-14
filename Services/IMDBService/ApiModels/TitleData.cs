using System.Collections.Generic;

namespace Services.IMDBService.Models
{
    public class TitleData
    {
        public string Id { get; set; }
        public string Title { set; get; }
        public string OriginalTitle { get; set; }
        public string FullTitle { set; get; }
        public string Year { set; get; }
        public string ReleaseDate { set; get; }
        public string RuntimeMins { set; get; }
        public string RuntimeStr { set; get; }
        public string Plot { set; get; } // IMDb Plot allways en, TMDb Plot translate
        public string PlotLocal { set; get; }
        public bool PlotLocalIsRtl { set; get; }
        public string Awards { set; get; }
        public string Image { get; set; }
        public string Type { set; get; }
        public string Directors { set; get; }
        public List<StarShort> DirectorList { get; set; }
        public string Writers { set; get; }
        public List<StarShort> WriterList { get; set; }
        public string Stars { set; get; }
        public List<StarShort> StarList { get; set; }
        public List<ActorShort> ActorList { get; set; }
        public FullCastData FullCast { get; set; }
        public string Genres { set; get; }
        public List<KeyValueItem> GenreList { get; set; }
        public string Companies { get; set; }
        public List<CompanyShort> CompanyList { get; set; }
        public string Countries { set; get; }
        public List<KeyValueItem> CountryList { set; get; }
        public string Languages { set; get; }
        public List<KeyValueItem> LanguageList { set; get; }
        public string ContentRating { get; set; }
        public string IMDbRating { get; set; }
        public string IMDbRatingVotes { get; set; }
        public string MetacriticRating { set; get; }
        public RatingData Ratings { set; get; }
        public WikipediaData Wikipedia { set; get; }
        public PosterData Posters { get; set; }
        public ImageData Images { get; set; }
        public TrailerData Trailer { get; set; }
        public BoxOfficeShort BoxOffice { get; set; }
        public string Tagline { get; set; }
        public string Keywords { get; set; }
        public List<string> KeywordList { get; set; }
        public List<SimilarShort> Similars { get; set; }
        public TvSeriesInfo TvSeriesInfo { get; set; }
        public TvEpisodeInfo TvEpisodeInfo { get; set; }
        public string ErrorMessage { get; set; }
    }
}
