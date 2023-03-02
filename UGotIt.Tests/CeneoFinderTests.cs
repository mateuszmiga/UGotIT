using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraper.Finder;
using WebScraper.Models;
using Xunit.Abstractions;

namespace UGotIt.Tests
{
    public class CeneoFinderTests
    {
        private readonly ITestOutputHelper output;

        public CeneoFinderTests(ITestOutputHelper output)
        {
            this.output = output;
        }


        [Theory]
        [InlineData("Xbox series one")]
        [InlineData("PHILIPS Ovi Smart")]
        [InlineData("Brother InkBenefit Plus")]
        [InlineData("Iphone 13 128gb")]
        public void FindProduct_TrueProducts_ShouldReturnProducts(string productName)
        {
            //Arrange
            var finder = new CeneoFinder();

            //Act
            var products = finder.FindProduct(productName);

            output.WriteLine("Found : " + products.Count.ToString());
            foreach (var item in products)
            {
                output.WriteLine(item.ProductName);
            }

            //Assert
            products.Should().NotBeEmpty();
            products.Should().NotBeNull();
        }
    }
}
