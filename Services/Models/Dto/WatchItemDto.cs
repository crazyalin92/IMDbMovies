using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models
{
    public class WatchItemDto : MovieDto
    {
        public int WatchItemId { get; set; }
        public int UserId { get; set; }
        public bool IsWatched { get; set; }
    }
}
