using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraper.Models;
using WebScraper.XkomScraper.Models;

namespace WebScraper.XkomScraper
{
    public class XkomScraper : IDataScraper
    {
        public ICollection<WebScraper.Models.Review> GetReviews(string productUrl)
        {
            var reviews = ExtractReviewsFromProductPage(productUrl);
            return reviews;
        }

        private ICollection<WebScraper.Models.Review> ExtractReviewsFromProductPage(string url)
        {
            if (url != string.Empty)
            {
                var web = new HtmlWeb();
                var reviews = new List<WebScraper.Models.Review>();
                var doc = web.Load(url);
                var reviewNodes = doc.QuerySelector("div.sc-1re71we-0.fOThDJ > script");

                try
                {
                    Root rootObj = JsonConvert.DeserializeObject<Root>(reviewNodes.InnerHtml);

                    foreach (var reviewNode in rootObj.review)
                    {
                        var review = new WebScraper.Models.Review();
                        review.UserName = reviewNode.author.name;
                        review.ReviewContent = reviewNode.description;
                        review.SourcePage = url;
                        review.Rating = reviewNode.reviewRating.ratingValue.ToString();

                        reviews.Add(review);
                    }

                    return reviews;
                }
                catch (Exception)
                {

                    return new List<WebScraper.Models.Review>();
                }
                
            }
            else
            {
                return new List<WebScraper.Models.Review>(); //return empty collection
            }
        }
    }
}
