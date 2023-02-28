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
        public ICollection<Review> GetReviews(string productName)
        {            
            var url = ReturnProductPageUrl(productName);
            var reviews = ExtractReviewsFromProductPage(url); 
            return reviews;
        }

        private string ReturnProductPageUrl(string productName)
        {
            //strategy pattern
            IFinder ceneoFinder = new CeneoFinder();
            IFinder googleFinder = new GoogleFinder();

            ProductFinder productFinder = new ProductFinder(ceneoFinder);
            var url = productFinder.FindProduct(productName);

            if (url != string.Empty)
            {
                return url;
            }
            else
            {
                productFinder.SetStrategy(googleFinder);
                url = productFinder.FindProduct(productName);

                return url;
            }
        }

        private ICollection<Review> ExtractReviewsFromProductPage(string url)
        {
            if (url != string.Empty)
            {
                var web = new HtmlWeb();
                var reviews = new List<Review>();
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
    }
}
