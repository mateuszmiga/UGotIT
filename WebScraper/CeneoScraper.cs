using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraper.Models;

namespace WebScraper
{
    public class CeneoScraper : IDataScraper
    {
        public ICollection<Review> GetReviews(string productUrl)
        {
            var reviews = ExtractReviewsFromProductPage(productUrl);
            return reviews;
        }

        private ICollection<Review> ExtractReviewsFromProductPage(string url)
        {
            if (url != string.Empty)
            {
                var web = new HtmlWeb();
                var reviews = new List<Review>();
                var doc = web.Load(url);
                var reviewNodes = doc.QuerySelectorAll(".user-post.user-post__card.js_product-review");

                try
                {
                    foreach (var reviewNode in reviewNodes)
                    {
                        var review = new Review();
                        review.UserName = reviewNode.QuerySelector(".user-post__author-name").InnerText.Remove(0, 1);
                        review.ReviewContent = reviewNode.QuerySelector(".user-post__text").InnerText;
                        review.SourcePage = url;
                        review.Rating = reviewNode.QuerySelector(".user-post__score-count").InnerText;

                        reviews.Add(review);
                    }
                }
                catch (Exception)
                {

                    return new List<Review>();
                }

                

                return reviews;
            }
            else
            {
                return new List<Review>(); //return empty collection
            }
        }
    }
}
