using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraper.Finder;
using WebScraper.Models;

namespace WebScraper.Finder
{
    internal class ProductFinder : IFinder
    {
        private IFinder _finder;

        internal ProductFinder(IFinder strategy)
        {
            _finder = strategy;
        }

        internal void SetStrategy(IFinder strategy)
        {
            _finder = strategy;
        }

        public ICollection<Product> FindProduct(string productName, ShopName shopName)
        {
            return _finder.FindProduct(productName, shopName);
        }
    }
}
