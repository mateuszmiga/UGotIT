using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraper.XkomScraper.Models;

namespace WebScraper.KomputronikScraper.ModelsKomputronik
{
    public class JsonModels
    {
        public class AggregateRating
        {
            [JsonProperty("@type")]
            public string type { get; set; }
            public double ratingValue { get; set; }
            public int reviewCount { get; set; }
        }

        public class Brand
        {
            [JsonProperty("@type")]
            public string type { get; set; }
            public string name { get; set; }
        }

        public class Offers
        {
            [JsonProperty("@type")]
            public string type { get; set; }
            public int price { get; set; }
            public string priceCurrency { get; set; }
            public string priceValidUntil { get; set; }
            public string itemCondition { get; set; }
            public string availability { get; set; }
            public string url { get; set; }
        }

        public class Root
        {
            [JsonProperty("@context")]
            public string context { get; set; }

            [JsonProperty("@type")]
            public string type { get; set; }
            public string name { get; set; }
            public string sku { get; set; }
            public string image { get; set; }
            public string mpn { get; set; }
            public string gtin13 { get; set; }
            public string description { get; set; }
            public Brand brand { get; set; }
            public AggregateRating aggregateRating { get; set; }
            public Offers offers { get; set; }
        }
    }
}
