using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebScraper.Models;

namespace OpinionMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApiClient _client;

        public ProductController(ApiClient client)
        {
            _client = client;
        }

        public async Task<ActionResult> GetProducts(string searchTerm)
        {            
            var results = await _client.GetProducts(searchTerm);
            return View(results);                
        }
    }
}
