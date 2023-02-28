using System;
using WebScraper;
using WebScraper.AmazonScraper;
using WebScraper.Finder;
using WebScraper.Models;

namespace UGotIT.Services
{
    public class ReviewService : IReviewService
    {
        IDataScraper Allegro = new AllegroScraper();
        IDataScraper Amazon = new AmazonScraper();
        IDataScraper Ceneo = new CeneoScraper();
        IDataScraper Opineo = new OpineoScraper();
        IDataScraper Xkom = new XkomScraper();



        public ICollection<Review> GetAllReviews(string productUrl)
        {
            List<Review> reviews = new List<Review>();

            // reviews.AddRange(Allegro.GetReviews(productName));
            reviews.AddRange(Amazon.GetReviews(productUrl));
            //reviews.AddRange(Ceneo.GetReviews(productName));
            //reviews.AddRange(Opineo.GetReviews(productName));
            // reviews.AddRange(Xkom.GetReviews(productName));

            return reviews;
        }

        public ICollection<Product> GetProducts(string productName)
        {
            var ceneoFinder = new CeneoFinder();
            var products = ceneoFinder.FindProduct(productName);

            return products;
            
        }
    }
}

                

            
        
    

