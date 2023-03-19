using HtmlAgilityPack;
using Newtonsoft.Json;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using WebScraper.XkomScraper.Models;
using WebScraper.Models;

namespace WebScraper.XkomScraper
{
    public class XkomScraper : IDataScraper
    {
        public async Task<ICollection<WebScraper.Models.Review>> GetReviewsAsync(string productUrl)
        {
            var reviews = ExtractReviewsFromProductPage(productUrl);
            return await reviews;
        }

        private async Task<ICollection<WebScraper.Models.Review>> ExtractReviewsFromProductPage(string url)
        {
            if (url != string.Empty)
            {
                var web = new HtmlWeb();
                var reviews = new List<WebScraper.Models.Review>();
                var doc = web.Load(url);
                var reviewNodes = doc.QuerySelector("div.sc-1re71we-0.fOThDJ > script");

                try
                {
                    Root rootObj = JsonConvert.DeserializeObject<Root>(reviewNodes.InnerHtml);

                    foreach (var reviewNode in rootObj.review)
                    {
                        var review = new WebScraper.Models.Review();
                        review.UserName = reviewNode.author.name;
                        review.ReviewContent = reviewNode.description;
                        review.SourcePage = url;
                        review.Rating = reviewNode.reviewRating.ratingValue.ToString();

                        reviews.Add(review);
                    }

                    return reviews;
                }
                catch (Exception)
                {

                    return new List<WebScraper.Models.Review>();
                }
                
            }
            else
            {
                return new List<WebScraper.Models.Review>(); //return empty collection
            }
        }

        private ICollection<WebScraper.Models.Review> ExtractReviewsFromProductPageWithSelenium(string url)
        {

            if (url != string.Empty)
            {
                var reviews = new List<WebScraper.Models.Review>();

                // HtmlAgilityPack
                var web = new HtmlWeb();
                var doc = web.Load(url);
                var reviewNodes = doc.QuerySelector("div.sc-1re71we-0.fOThDJ > script");
                Root rootObj = JsonConvert.DeserializeObject<Root>(reviewNodes.InnerHtml);

                var number = rootObj.aggregateRating.reviewCount;
                double loopsNumber = Math.Round(((double)(number - 15) / 15));

                // Selenium
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--headless");

                IWebDriver driver = new ChromeDriver(options);
                driver.Navigate().GoToUrl(url);

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                for (int i = 0; i < loopsNumber; i++)
                {
                    js.ExecuteScript("document.querySelector('button.sc-15ih3hi-0.sc-6i4pc6-0.bVwUtI.sc-1lp3a37-7.gRZnCa').click();");
                    System.Threading.Thread.Sleep(2000);
                }

                WebElement scriptElement = (WebElement)driver.FindElement(By.CssSelector("div.sc-1re71we-0.fOThDJ > script"));

                string scriptContent = scriptElement.GetAttribute("innerHTML");

                try
                {
                    Root reviewNodes_ = JsonConvert.DeserializeObject<Root>(scriptContent);

                    foreach (var reviewNode in reviewNodes_.review)
                    {
                        var review = new WebScraper.Models.Review();
                        review.UserName = reviewNode.author.name;
                        review.ReviewContent = reviewNode.description;
                        review.SourcePage = url;
                        review.Rating = reviewNode.reviewRating.ratingValue.ToString();

                        reviews.Add(review);
                    }

                    return reviews;
                }
                catch (Exception)
                {

                    return new List<WebScraper.Models.Review>();
                }
            }
            else
            {
                return new List<WebScraper.Models.Review>(); //return empty collection
            }
            
        }

    }
}
