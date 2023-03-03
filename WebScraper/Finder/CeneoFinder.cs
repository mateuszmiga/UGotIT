using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraper.Models;

namespace WebScraper.Finder
{
    public class CeneoFinder
    {
        private const string BaseUrl = "https://www.ceneo.pl/;szukaj-";
        private const string BaseUrlParam = "?nocatnarrow=1";

        public ICollection<Product> FindProduct(string productName)
        {
            var searchQuery = productName.Split(' '); 
            var web = new HtmlWeb();
            var formattedProductName = productName.Replace(' ', '+');

            var ceneoSearchResultPage = web.Load(BaseUrl + formattedProductName + BaseUrlParam);
            var results = ceneoSearchResultPage.DocumentNode.SelectNodes("//*[@id=\"body\"]/div/div/div[3]/div/section/div[3]/div").Take(8);

            ICollection<Product> products = new List<Product>();
            
            foreach (var item in results)
            {
                if (!item.InnerHtml.ToLower().Contains(searchQuery[0].ToLower()))
                {
                    continue;
                }
                
                try
                {
                    Product product = new();
                    product.Url = "https://www.ceneo.pl" + item.QuerySelector("a").Attributes["href"].Value;
                    product.PhotoUrl = item.QuerySelector("img").Attributes["src"].Value;
                    product.ProductName = item.QuerySelector("img").Attributes["alt"].Value;

                    products.Add(product);
                }
                catch (Exception)
                {
                    
                }                
            }
            return products;
        }
    }
}
