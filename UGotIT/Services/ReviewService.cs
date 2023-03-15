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
        IDataScraper Xkom = new XkomScraper();



        public async Task<ICollection<Review>> GetAllReviewsAsync(string productUrl)
        {
            List<Review> reviews = new List<Review>();
                        
            ProductUrlGenerator generator = new ProductUrlGenerator();
            
            var pages =await generator.ReturnProductPages(productUrl);

            reviews.AddRange(await Amazon.GetReviewsAsync(pages.AmazonUrl));
            reviews.AddRange(await Komputronik.GetReviewsAsync(pages.KomputronikUrl));
            reviews.AddRange(await Ceneo.GetReviewsAsync(pages.CeneoUrl));            
            reviews.AddRange(await Xkom.GetReviewsAsync(pages.XkomUrl));

            return reviews;
        }

        public async Task<ICollection<Product>> GetProductsAsync(string productName)
        {
            var ceneoFinder = new CeneoFinder();
            var products = ceneoFinder.FindProduct(productName);

            return await products;
            
        }
    }
}

                

            
        
    

