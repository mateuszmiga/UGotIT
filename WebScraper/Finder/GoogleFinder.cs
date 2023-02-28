using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraper.Models;


namespace WebScraper.Finder
{
    public class GoogleFinder
    {
        private const string BaseUrl = "https://www.google.pl/search?q=";

       
        public ICollection<Product> FindProduct(string productName, string shopName)
        {
            var web = new HtmlWeb();
            var formattedProductName = productName.Replace(' ', '+') + " " + shopName.ToString();

            var googleSearchResultPage = web.Load(BaseUrl + formattedProductName);
            var urlsNodes = googleSearchResultPage.QuerySelectorAll("div.Z26q7c.UK95Uc.jGGQ5e > div > a").Take(3);
            var photoUrls = googleSearchResultPage.QuerySelectorAll("div.LicuJb.uhHOwf.BYbUcd"); //TODO
            var names = googleSearchResultPage.QuerySelectorAll("div.Z26q7c.UK95Uc.jGGQ5e > div > a"); //TODO

            List<string> urls = new();
            foreach (var item in urlsNodes)
            {
                var url = item.Attributes["href"].Value;
                if (url.Contains(shopName.ToString()))
                {
                    urls.Add(url);
                }
            }
            
            ICollection<Product> products= new List<Product>();

            foreach(var url in urls)
            {
                Product product = new Product();
                product.Url = url;
                products.Add(product);
            }

            return products.Take(1).ToList();
        }
    }
}
