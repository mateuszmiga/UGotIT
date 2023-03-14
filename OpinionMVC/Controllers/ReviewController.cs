using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpinionMVC.Models;

namespace OpinionMVC.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ApiClient _client;

        public ReviewController(ApiClient client)
        {
            _client = client;
        }

        
        public async Task<ActionResult> GetReviews(string url)
        {
            var result = await _client.GetReviews(url);
            
            return View(result);
        }        
    }
}
