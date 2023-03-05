using System;
using WebScraper;
using WebScraper.AmazonScraper;
using WebScraper.Finder;
using WebScraper.KomputronikScraper;
using WebScraper.Models;
using WebScraper.XkomScraper;

namespace UGotIT.Services
{
    public class ReviewService : IReviewService
    {
        IDataScraper Komputronik = new KomputronikScraper();
        IDataScraper Amazon = new AmazonScraper();
        IDataScraper Ceneo = new CeneoScraper();
        IDataScraper Opineo = new OpineoScraper();
        IDataScraper Xkom = new XkomScraper();



        public ICollection<Review> GetAllReviews(string productUrl)
        {
            List<Review> reviews = new List<Review>();
                        
            ProductUrlGenerator generator = new ProductUrlGenerator();
            
            var pages = generator.ReturnProductPages(productUrl);

            reviews.AddRange(Amazon.GetReviews(pages.AmazonUrl));
            reviews.AddRange(Komputronik.GetReviews(pages.KomputronikUrl));
            reviews.AddRange(Ceneo.GetReviews(pages.CeneoUrl));            
            reviews.AddRange(Xkom.GetReviews(pages.XkomUrl));

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

                

            
        
    

