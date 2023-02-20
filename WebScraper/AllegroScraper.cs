using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraper.Models;

namespace WebScraper
{
    public class AllegroScraper : IDataScraper
    {

        public ICollection<Review> Reviews { get; set; }

        public string GetUrl(string productName)
        {
            string productUrl = productName;
            return productUrl;
        }

        public ICollection<Review> GetReviews(string productName)
        {

            var productUrl = GetUrl(productName);
            
            var web = new HtmlWeb();
            var document = web.Load(productUrl);

            var reviews = document.QuerySelectorAll(".mx7m_1 mlkp_ag m911_co mp4t_16 mp4t_24_l mg9e_16 mg9e_24_l");



            return Reviews;
        }
    }
}
