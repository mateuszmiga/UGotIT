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
        [InlineData("https://www.amazon.pl/Bluetooth-SoundLink-bezprzewodowy-wodoodporny-zewnêtrzny/dp/B099TJGJ91")]
        [InlineData("https://www.amazon.pl/Sony-WH-1000XM5-bezprzewodowe-zoptymalizowane-telefonicznych/dp/B09Y2MYL5C")]
        [InlineData("https://www.amazon.pl/Apple-iPhone-13-128-GB-ksiê¿ycowa/dp/B09G9TSXPX")]
        public async Task GetReviews_TrueUrls_ShouldNotReturnEmptyReviewsAsync(string productUrl)
        {
            //arrange
            AmazonScraper scraper = new();            
            
            //Act
            var revs = await scraper.GetReviewsAsync(productUrl);

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
            AmazonScraper scraper = new();

            //Act
            var revs =await scraper.GetReviewsAsync(productUrl);
           
            //assert
            Assert.Empty(revs);
        }
    }
}