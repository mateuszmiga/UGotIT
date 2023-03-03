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
    public class AllegroScraperTests
    {
        private readonly ITestOutputHelper output;

        public AllegroScraperTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [InlineData("https://allegro.pl/oferta/akumulatorowa-pilarka-tarczowa-18v-165mm-makita-12540426704")]
        [InlineData("https://allegro.pl/oferta/kamera-zewnetrzna-wi-fi-full-hd-obrotowa-4x-zoom-13203712337")]
        [InlineData("https://allegro.pl/oferta/roborock-s7-czarny-robot-sprzatajacy-odkurzacz-10868803887")]
        public void GetReviews_TrueUrls_ShouldNotReturnEmptyReviews(string productUrl)
        {
            //arrange
            AllegroScraper scraper = new();

            //Act
            var revs = scraper.GetReviews(productUrl);

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
        public void GetReviews_FakeUrls_ShouldReturnEmptyReviews(string productUrl)
        {
            //arrange
            AllegroScraper scraper = new();

            //Act
            var revs = scraper.GetReviews(productUrl);

            //assert
            Assert.Empty(revs);
        }
    }
}
