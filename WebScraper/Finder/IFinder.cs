using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraper.Models;

namespace WebScraper.Finder
{
    internal interface IFinder
    {
        ICollection<Product> FindProduct(string productName, ShopName shopName);
    }
}
