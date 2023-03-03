using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper.XkomScraper.Models
{
    public class Review
    {
        [JsonProperty("@type")]
        public string type { get; set; }
        public DateTime datePublished { get; set; }
        public string description { get; set; }
        public Author author { get; set; }
        public ReviewRating reviewRating { get; set; }
    }
}
