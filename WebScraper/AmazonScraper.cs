using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraper.Models;

namespace WebScraper
{
    public class AmazonScraper : IDataScraper
    {
        private const string BaseUrl = "https://www.amazon.pl/s?k=";
        

        public ICollection<Review> GetReviews(string productName)
        {
            var web = new HtmlWeb();
            var doc = web.Load(ReturnProductPageUrl(productName));

            var content = doc.QuerySelectorAll("span[\"global-reviews-all\"]");
            throw new NotImplementedException();
        }

        private static string ReturnProductPageUrl(string productName) 
        {
            var web = new HtmlWeb();
            var formattedProductName = productName.Replace(' ', '+');

            try
            {
                var doc = web.Load(BaseUrl + formattedProductName);
                var url = doc.DocumentNode.SelectNodes("//*[@id=\"search\"]/div[1]/div[1]/div/span[1]/div[1]/div[6]/div/div/div/div/div[2]/div[1]/h2/a").FirstOrDefault();
                return url.QuerySelector("a").Attributes["href"].Value;
            }
            catch (Exception)
            {
                return "can't find product!";
            }            
        }
    }
}
