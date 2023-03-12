using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using UGotIT.Services;
using WebScraper.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UGotIT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }


        // GET: api/<ReviewController>
        [HttpGet]
        public IEnumerable<Review> Get(string productUrl)
        {
            IEnumerable<Review> reviews = _reviewService.GetAllReviews(productUrl);
            
            return reviews;
        } 
    }
}
