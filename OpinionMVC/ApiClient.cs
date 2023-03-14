using Newtonsoft.Json;
using Flurl;
using Flurl.Http;

using OpinionMVC.Models;

namespace OpinionMVC
{
    public class ApiClient
    {
        private readonly string baseUrlProductEndpoint = "https://localhost:7042/api/Product?productName=";
        private readonly string baseUrlReviewEndpoint = "https://localhost:7042/api/Review?productUrl=";
        HttpClient client = new HttpClient();

        public async Task<List<Product>> GetProducts(string searchTerm)
        {
            try
            {
                var response = await client.GetStringAsync(baseUrlProductEndpoint + searchTerm);
                var result = JsonConvert.DeserializeObject<List<Product>>(response);

                return result;

            }
            catch (Exception)
            {
                return new List<Product>();
            }

        }

        public async Task<List<Review>> GetReviews(string productUrl)
        {
            try
            {
                var response = await client.GetStringAsync(baseUrlReviewEndpoint + productUrl);
                var result = JsonConvert.DeserializeObject<List<Review>>(response);

                return result;

            }
            catch (Exception)
            {
                return new List<Review>();
            }

        }
    }
}
