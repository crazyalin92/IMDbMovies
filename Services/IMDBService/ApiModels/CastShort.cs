using System.Collections.Generic;

namespace Services.IMDBService.Models
{
    public class CastShort
    {
        public string Job { get; set; }
        public List<CastShortItem> Items { get; set; }
    }
}