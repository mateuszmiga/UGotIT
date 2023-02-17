using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraper.Models;

namespace WebScraper
{
    public class AmazonScraper : IDataScraper
    {
        public ICollection<Review> GetReviews(string productName)
        {
            throw new NotImplementedException();
        }
    }
}
