using Microsoft.AspNetCore.Mvc;
using UGotIT.Services;
using WebScraper.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UGotIT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IReviewService _reviewService;

        public ProductController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<Product> GetProducts(string productName)
        {
            IEnumerable<Product> products = _reviewService.GetProducts(productName);

            return products;
        }

    }
}
