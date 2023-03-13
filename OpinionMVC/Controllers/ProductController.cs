using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebScraper.Models;

namespace OpinionMVC.Controllers
{
    public class ProductController : Controller
    {
        
        public ActionResult GetProducts(string searchTerm)
        {
            var model = new Product();
            var results = new List<Product>();
            results.Add(model);
            return View(results);                
        }
    }
}
