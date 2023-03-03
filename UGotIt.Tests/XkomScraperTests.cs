using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraper;
using WebScraper.AmazonScraper;
using WebScraper.XkomScraper;
using Xunit.Abstractions;

namespace UGotIt.Tests
{
    public class XkomScraperTests
    {
        private readonly ITestOutputHelper output;

        public XkomScraperTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Theory]
        [InlineData("https://www.x-kom.pl/p/1073277-dysk-ssd-wd-1tb-m2-pcie-gen4-nvme-black-sn850x.html")]
        [InlineData("https://www.x-kom.pl/p/728431-smartfon-telefon-xiaomi-redmi-note-11s-6-64gb-graphite-gray.html")]
        [InlineData("https://www.x-kom.pl/p/562889-dysk-zewnetrzny-ssd-samsung-portable-ssd-t7-1tb-usb-32-gen-2-czerwony.html")]
        public void GetReviews_TrueUrls_ShouldNotReturnEmptyReviews(string productUrl)
        {
            //arrange
            XkomScraper scraper = new();

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
            XkomScraper scraper = new();

            //Act
            var revs = scraper.GetReviews(productUrl);

            //assert
            Assert.Empty(revs);
        }
    }
}
