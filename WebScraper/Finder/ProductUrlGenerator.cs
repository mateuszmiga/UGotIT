﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraper.Models;

namespace WebScraper.Finder
{
    public class ProductUrlGenerator
    {
        public async Task<ProductPages> ReturnProductPages(string ceneoProductUrl)
        {
            ProductPages productPages = new ProductPages();            

            productPages.CeneoUrl = ceneoProductUrl;
            var web = new HtmlWeb();
            var ceneoProductPage = web.Load(ceneoProductUrl);
            
            var productNodes = ceneoProductPage.QuerySelectorAll("#click > div:nth-child(2) > section.product-offers.product-offers--standard > ul > li ");

            productPages.AmazonUrl =await CheckOffersAndReturnUrlAsync(productNodes, "amazon.pl");
            productPages.XkomUrl = await CheckOffersAndReturnUrlAsync(productNodes, "x-kom");
            productPages.KomputronikUrl = await CheckOffersAndReturnUrlAsync(productNodes, "komputronik");

            return productPages;
                
        }

        private async Task<string> CheckOffersAndReturnUrlAsync(IList<HtmlNode> productNodes, string shop)
        {
            
            if (productNodes.Any(n => n.OuterHtml.Contains(shop)))
            {
                var amazonNodeUrl = productNodes.Where(o => o.OuterHtml.Contains(shop))
                    .FirstOrDefault().QuerySelector("a").Attributes["href"].Value;

                var web = new HtmlWeb();
                var redirectionPage = "https://www.ceneo.pl" + amazonNodeUrl;
                var productPage = web.Load(redirectionPage)
                    .QuerySelectorAll("meta")
                    .Where(s => s.OuterHtml.Contains(shop))
                    .First()
                    .OuterHtml
                    .Split("url=")
                    .FirstOrDefault(s => s.Contains(shop))
                    .Split("?")
                    .First();
                return productPage;
            }
            else
            {                
                var product = productNodes.FirstOrDefault().QuerySelector("div.product-offer__product__offer-details__name > a > span").InnerHtml;
                
                GoogleFinder googleFinder = new GoogleFinder();
                var result = await googleFinder.FindProduct(product, shop);
                try
                {
                    return result.FirstOrDefault().Url;
                }
                catch (Exception)
                {

                    return string.Empty;
                }
                
            }
        }
    }
}
        
        
