using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ROMLoader.Models;

namespace ROMLoader.tests
{
    [TestFixture]
    class StockpileTest
    {

        [Test]
        public void ToStringTest()
        {
            Stockpile stockpile = new Stockpile("1", "24S_25_J28");

            Assert.True(stockpile.ToString() == "Stockpile 1 : 24S_25_J28");
        }

        [Test]
        public void EqualsTest()
        {
            Stockpile stockpile = new Stockpile("1", "24S_25_J28");
            Stockpile stockpile1 = new Stockpile("1", "24S_25_J28");
            Stockpile stockpile2 = new Stockpile("2", "24S_25_J28");
            Stockpile stockpile3 = new Stockpile("1", "12S_12_H28");

            //Test for two equal stockpiles.
            Assert.True(stockpile.Equals(stockpile1));

            //Test for when stockpile number is different, same coal.
            Assert.False(stockpile.Equals(stockpile2));

            //Test for when same stockpile number, different coal.
            Assert.False(stockpile.Equals(stockpile3));

            //Test for different stockpile number and different coal.
            Assert.False(stockpile2.Equals(stockpile3));
            
        }

        [Test]
        public void CompareToTest()
        {
            Stockpile stockpile = new Stockpile("1", "24S_25_J28");
            Stockpile stockpile1 = new Stockpile("1", "24S_25_J28");
            Stockpile stockpile2 = new Stockpile("2", "24S_25_J28");
            Stockpile stockpile3 = new Stockpile("3", "12S_12_H28");

            // Test for same position.
            Assert.Zero(stockpile.CompareTo(stockpile1));

            //Test for when the object preceeds the checked object.
            Assert.Negative(stockpile.CompareTo(stockpile2));

            //Test for when the object follows the checked item.
            Assert.Positive(stockpile3.CompareTo(stockpile));
        }
    }
}
