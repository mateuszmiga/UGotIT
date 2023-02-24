using WebScraper.AmazonScraper;
using WebScraper.Models;
using Xunit.Abstractions;

namespace UGotIt.Tests
{
    public class AmazonScraperTests
    {
        private readonly ITestOutputHelper output;

        public AmazonScraperTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [InlineData("xbox series x")]
        [InlineData("playstation 5")]
        [InlineData("iphone 13")]
        public void GetReviews_ShouldReturnNotEmptyReviews(string productName)
        {
            //arrange
            AmazonScraper scraper = new();            
            
            //Act
            var revs = scraper.GetReviews(productName);

            foreach (var item in revs)
            {
                output.WriteLine($"{item.UserName} {item.Rating}");
                output.WriteLine($"{item.SourcePage}");
                output.WriteLine($"======================================");
            }
            //assert
            Assert.NotEmpty(revs);
        }
    }
}