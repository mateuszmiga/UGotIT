using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper.XkomScraper.Models
{
    public class AggregateRating
    {
        [JsonProperty("@type")]
        public string type { get; set; }
        public double ratingValue { get; set; }
        public int reviewCount { get; set; }
        public int worstRating { get; set; }
        public int bestRating { get; set; }
    }
}
