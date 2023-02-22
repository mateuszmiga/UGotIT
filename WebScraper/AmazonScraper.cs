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
        private const string BaseUrl = "https://www.ceneo.pl/;szukaj-";
        private const string BaseUrlParam = "?nocatnarrow=1";

        public ICollection<Review> GetReviews(string productName)
        {
            if (productName != string.Empty)
            {
                var reviews = new List<Review>();

                var web = new HtmlWeb();
                var url = ReturnProductPageUrl(productName);
                var doc = web.Load(url);

                var reviewNodes = doc.QuerySelectorAll(".a-section.review.aok-relative");

                foreach (var reviewNode in reviewNodes)
                {
                    var review = new Review();
                    review.UserName = reviewNode.QuerySelector(".a-profile-name").InnerText;
                    review.ReviewContent = reviewNode.QuerySelector(".a-expander-content.reviewText.review-text-content.a-expander-partial-collapse-content").QuerySelector("span").InnerText;
                    review.SourcePage = url;
                    review.Rating = reviewNode.QuerySelector(".a-icon-alt").InnerText;

                    reviews.Add(review);
                }

                return reviews;
            }
            else
            {
                return new List<Review>(); //return empty collection
            }
        }

        private static string ReturnProductPageUrl(string productName) 
        {
            var web = new HtmlWeb();
            var formattedProductName = productName.Replace(' ', '+');

            var ceneoSearchResultPage = web.Load(BaseUrl + formattedProductName + BaseUrlParam);
            var url = ceneoSearchResultPage.DocumentNode.SelectNodes("//*[@id=\"body\"]/div/div/div[3]/div/section/div[3]/div[1]/div/div[2]/div[1]/div[1]/div[1]/strong/a").FirstOrDefault();


            var ceneoProductUrl = $"https://www.ceneo.pl{url.QuerySelector("a").Attributes["href"].Value}";

            var ceneoProductPage = web.Load(ceneoProductUrl);
            try
            {
                var productNodes = ceneoProductPage.QuerySelectorAll("#click > div:nth-child(2) > section.product-offers.product-offers--standard > ul > li ");

                if (productNodes.Any(n => n.OuterHtml.Contains("amazon")))
                {
                    var amazonNodeUrl = productNodes.Where(o => o.OuterHtml.Contains("amazon"))
                .FirstOrDefault()
                .QuerySelector("a").Attributes["href"].Value;

                    var redirectionPage = "https://www.ceneo.pl" + amazonNodeUrl;
                    var amazonProductPage = web.Load(redirectionPage)
                        .QuerySelectorAll("meta")
                        .Where(s => s.OuterHtml.Contains("amazon"))
                        .First()
                        .OuterHtml
                        .Split("url=")
                        .FirstOrDefault(s => s.Contains("amazon"))
                        .Split("?")
                        .First();
                    return amazonProductPage;
                }
                else
                {
                    return string.Empty;
                }
            }

            catch (NullReferenceException)
            {
                return string.Empty;
            }
        }
    }
}
