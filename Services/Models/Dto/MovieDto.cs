using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models
{
    public class MovieDto
    {
        public int MovieId { get; set; }
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public string WikiDescription { get; set; }
        public string ImdbRating { get; set; }
        public string PosterUrl { get; set; }
    }
}
