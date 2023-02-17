using WebScraper.Models;

namespace UGotIT.Services
{
    public interface IReviewService
    {
        public ICollection<Review> GetAllReviews(string productName);


    }
}
