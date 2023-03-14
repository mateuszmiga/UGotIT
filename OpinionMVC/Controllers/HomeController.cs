using Microsoft.AspNetCore.Mvc;
using OpinionMVC.Models;
using System.Diagnostics;

namespace OpinionMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApiClient _client;



        public HomeController(ILogger<HomeController> logger, ApiClient client)
        {
            _logger = logger;
            _client = client;
        }

        public async Task<ActionResult> GetProducts(SearchModel model)
        {
            var results = await _client.GetProducts(model.query);
            return View(results);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
    }
}