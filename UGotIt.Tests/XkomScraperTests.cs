﻿using System;
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
        [InlineData("https://www.x-kom.pl/p/1051988-soundbar-jbl-sb160.html")]
        public async Task GetReviews_TrueUrls_ShouldNotReturnEmptyReviewsAsync(string productUrl)
        {
            //arrange
            XkomScraper scraper = new();

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
            XkomScraper scraper = new();
            var revs = await scraper.GetReviewsAsync(productUrl);
            Assert.Empty(revs);
        }
    }
}
