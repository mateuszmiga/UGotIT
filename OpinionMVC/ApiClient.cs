using Newtonsoft.Json;
using Flurl;
using Flurl.Http;
using WebScraper.Models;

namespace OpinionMVC
{
    public class ApiClient
    {
        private readonly string baseUrl = "https://localhost:7042/api/Product?productName=";
        HttpClient client = new HttpClient();

        public async Task<List<Product>> GetProducts(string searchTerm)
        {
            try
            {
                var response = await client.GetStringAsync(baseUrl + searchTerm);
                var result = JsonConvert.DeserializeObject<List<Product>>(response);

                return result;

            }
            catch (Exception)
            {
                return new List<Product>();
            }

        }
    }
}
