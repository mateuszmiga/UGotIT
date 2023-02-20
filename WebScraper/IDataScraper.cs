using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraper.Models;

namespace WebScraper
{
    public interface IDataScraper
    {
        
        string GetUrl(string productName);
        public ICollection<Review> GetReviews(string productName);
    }
}
