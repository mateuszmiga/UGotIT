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
        
        public Task<ICollection<Review>> GetReviewsAsync(string productName);
    }
}
