using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraper.Models;

namespace WebScraper.XkomScraper.Models
{
    public class Root
    {
        [JsonProperty("@context")]
        public string context { get; set; }

        [JsonProperty("@type")]
        public string type { get; set; }
        public string name { get; set; }
        public string productID { get; set; }
        public string sku { get; set; }
        public string mpn { get; set; }

        public List<string> image { get; set; }

        public AggregateRating aggregateRating { get; set; }

        public List<Review> review { get; set; }

    }
}
