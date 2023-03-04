using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraper.Finder;
using WebScraper.Models;

namespace WebScraper.AmazonScraper
{
    public class AmazonScraper : IDataScraper
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
                var reviewNodes = doc.QuerySelectorAll(".a-section.review.aok-relative");

                try
                {
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
                catch (Exception)
                {

                    return new List<Review>();
                }

                
            }
            else
            {
                return new List<Review>(); //return empty collection
            }
        }
    }
}
