using WebScraper;
using WebScraper.AmazonScraper;
using WebScraper.Models;

namespace UGotIT.Services
{
    public class ReviewService : IReviewService
    {
        IDataScraper Allegro = new AllegroScraper();
        IDataScraper Amazon= new AmazonScraper();
        IDataScraper Ceneo= new CeneoScraper();
        IDataScraper Opineo= new OpineoScraper();
        IDataScraper Xkom= new XkomScraper();
        
        public ICollection<Review> GetAllReviews(string productName)
        {
            List<Review> reviews = new List<Review>();
            
           // reviews.AddRange(Allegro.GetReviews(productName));
            reviews.AddRange(Amazon.GetReviews(productName));
            //reviews.AddRange(Ceneo.GetReviews(productName));
            //reviews.AddRange(Opineo.GetReviews(productName));
           // reviews.AddRange(Xkom.GetReviews(productName));

            return reviews;
        }
    }
}
