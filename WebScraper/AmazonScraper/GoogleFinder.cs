using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper.AmazonScraper
{
    internal class GoogleFinder : IFinder
    {
        private const string BaseUrl = "https://www.google.pl/search?q=";        

        public string FindProduct(string productName)
        {
            var web = new HtmlWeb();
            var formattedProductName = productName.Replace(' ', '+') + "+amazon";

            var googleSearchResultPage = web.Load(BaseUrl + formattedProductName);
            var url = googleSearchResultPage.QuerySelector("div.Z26q7c.UK95Uc.jGGQ5e > div > a").Attributes["href"].Value;

            if (url.Contains("amazon"))
            {
                return url;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
