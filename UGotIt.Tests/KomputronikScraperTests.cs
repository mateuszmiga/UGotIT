using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraper.AmazonScraper;
using WebScraper.KomputronikScraper;
using Xunit.Abstractions;

namespace UGotIt.Tests
{
    public class KomputronikScraperTests
    {
        private readonly ITestOutputHelper output;

        public KomputronikScraperTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [InlineData("https://www.komputronik.pl/product/694900/philips-55pus9435-12.html")]
        [InlineData("https://www.komputronik.pl/product/705054/xiaomi-mi-true-wireless-earbuds-basic-2.html")]
        [InlineData("https://www.komputronik.pl/product/691513/de-longhi-dinamica-ecam-353-75-w.html")]
        public async Task GetReviews_TrueUrls_ShouldNotReturnEmptyReviewsAsync(string productUrl)
        {
            //arrange
            KomputronikScraper scraper = new();

            //Act
            var revs =await scraper.GetReviewsAsync(productUrl);

            foreach (var item in revs)
            {
                output.WriteLine($"{item.UserName} {item.Rating}");
                output.WriteLine($"{item.SourcePage}");
                output.WriteLine($"======================================");
            }
            //assert
            Assert.NotEmpty(revs);
        }


        [Theory]
        [InlineData("")]
        [InlineData("https://www.amazon.pl/")]
        [InlineData("https://www.ceneo.pl/123524030")]
        public async Task GetReviews_FakeUrls_ShouldReturnEmptyReviewsAsync(string productUrl)
        {
            //arrange
            KomputronikScraper scraper = new();

            //Act
            var revs =await scraper.GetReviewsAsync(productUrl);

            //assert
            Assert.Empty(revs);
        }
    }
}
