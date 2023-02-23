using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper.AmazonScraper
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

        public string FindProduct(string productName)
        {
            return _finder.FindProduct(productName);
        }
    }
}
