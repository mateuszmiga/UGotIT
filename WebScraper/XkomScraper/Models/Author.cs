using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper.XkomScraper.Models
{
    public class Author
    {
        [JsonProperty("@type")]
        public string type { get; set; }
        public string name { get; set; }
    }
}