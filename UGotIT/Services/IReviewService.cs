using WebScraper.Models;

namespace UGotIT.Services
{
    public interface IReviewService
    {
        public Task<ICollection<Review>> GetAllReviewsAsync(string productUrl);
        public Task<ICollection<Product>> GetProductsAsync(string productName);


    }
}
