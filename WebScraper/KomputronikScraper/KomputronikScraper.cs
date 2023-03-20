using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraper.Models;
using static WebScraper.KomputronikScraper.ModelsKomputronik.JsonModels;
using Review = WebScraper.Models.Review;

namespace WebScraper.KomputronikScraper
{
    public class KomputronikScraper : IDataScraper
    {
        public async Task<ICollection<Review>> GetReviewsAsync(string productUrl)
        {
            var reviews = ExtractReviewsFromProductPage(productUrl);
            return await reviews;
        }

        private async Task<ICollection<Review>> ExtractReviewsFromProductPage(string productUrl)
        {
            if (productUrl != string.Empty)
            {
                var web = new HtmlWeb();
                var reviews = new List<Review>();
                var doc = web.Load(productUrl);
                var reviewNodes = doc.QuerySelectorAll(".space-y-4.p-6").Skip(1);
                /*                var reviewNodes = doc.QuerySelectorAll("div.space-y-4.p-6").Skip(1);*/
                var Json = doc.QuerySelector("div.container > div > script");

                try
                {

                    var docJson = JsonConvert.DeserializeObject<Root>(Json.InnerHtml);

                    foreach (var reviewNode in reviewNodes)
                    {
                        var review = new Review();
                        review.ReviewContent =  reviewNode.QuerySelector("div.text-base.text-gray-gravel").InnerText.Trim();
                        review.Rating = reviewNode.QuerySelector("ktr-star-rating").Attributes["rating"].Value;
                        review.SourcePage = productUrl;
                        review.UserName = reviewNode.QuerySelector("span.font-semibold").InnerText.Trim();
                      
/*                        review.UserName = 
                        review.ReviewContent = reviewNode.QuerySelector(".a-expander-content.reviewText.review-text-content.a-expander-partial-collapse-content").QuerySelector("span").InnerText;
                        review.SourcePage = productUrl;*/
/*                        review.Rating = docJson.ratingValue.ToString();*/

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
