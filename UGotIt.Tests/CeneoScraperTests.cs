using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraper;
using WebScraper.AmazonScraper;
using Xunit.Abstractions;

namespace UGotIt.Tests
{
    public class CeneoScraperTests
    {
        private readonly ITestOutputHelper output;

        public CeneoScraperTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [InlineData("https://www.ceneo.pl/94823130")]
        [InlineData("https://www.ceneo.pl/86467784")]
        [InlineData("https://www.ceneo.pl/88072703")]
        public async Task GetReviews_TrueUrls_ShouldNotReturnEmptyReviewsAsync(string productUrl)
        {
            //arrange
            CeneoScraper scraper = new();

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
        [InlineData("https://www.ceneo.pl/")]
        public async Task GetReviews_FakeUrls_ShouldReturnEmptyReviewsAsync(string productUrl)
        {
            //arrange
            CeneoScraper scraper = new();

            //Act
            var revs =await scraper.GetReviewsAsync(productUrl);

            //assert
            Assert.Empty(revs);
        }
    }
}
