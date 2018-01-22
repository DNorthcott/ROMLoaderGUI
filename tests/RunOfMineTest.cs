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
    class RunOfMineTest
    {
        [Test]
        public void EqualsTest()
        {
            RunOfMine rom = new RunOfMine("2018-01-18", 1, "03S_46_J19", "13_34_F23",
                null, "03S_46_F25", "05N_46_F25", "13_36_J17", "16S_23_G53", "16N_22_F253", "12_34_F25", "13_36_J17");

            RunOfMine rom1 = new RunOfMine("2018-01-18", 1, "03S_46_J19", "13_34_F23",
                null, "03S_46_F25", "05N_46_F25", "13_36_J17", "16S_23_G53", "16N_22_F253", "12_34_F25", "13_36_J17");

            RunOfMine rom3 = new RunOfMine("2018-02-18", 1, "03S_46_J19", "13_34_F23",
                null, "03S_46_F25", "05N_46_F25", "13_36_J17", "16S_23_G53", "16N_22_F253", "12_34_F25", "13_36_J17");

            RunOfMine rom4 = new RunOfMine("2018-02-18", 2, "03S_46_J19", "13_34_F23",
                null, "03_46_F25", "05N_46_F25", "13_36_J17", "16S_23_G53", "16N_22_F253", "12_34_F25", "13_36_J17");

            RunOfMine rom5 = new RunOfMine("2018-02-18", 2, "03S_46_J19", "13_34_F23",
                null, "03S_46_F25", "05N_46_F25", "13_36_J17", "16S_23_G53", "16N_22_F253", "12_34_F25", "13_36_J17");

            RunOfMine rom6 = new RunOfMine("2018-02-18", 2, null, "13_34_F23",
                null, "03S_46_F25", "05N_46_F25", "13_36_J17", "16S_23_G53", "16N_22_F253", "12_34_F25", "13_36_J17");

            RunOfMine rom7 = new RunOfMine("2018-02-18", 2, null, "13_34_F23",
                null, "03S_46_F25", "05N_46_F25", "13_36_J17", "16S_23_G53", "16N_22_F253", "12_34_F25", null);

            RunOfMine rom8 = new RunOfMine("2018-02-18", 2, null, "13_34_F23",
                null, "03S_46_F25", "05N_46_F25", "13_36_J17", null, "16N_22_F253", "12_34_F25", null);

            //Test for equal object.
            Assert.True(rom.Equals(rom1));

            //Test for different dates.
            Assert.False(rom.Equals(rom3));

            //Test for different priority.
            Assert.False(rom3.Equals(rom4));

            //Test for different stockpile strings. Few random selections.
            Assert.False(rom5.Equals(rom6));
            Assert.False(rom5.Equals(rom7));
            Assert.False(rom5.Equals(rom8));
            Assert.False(rom6.Equals(rom7));
            Assert.False(rom6.Equals(rom8));
            Assert.False(rom7.Equals(rom8));


        }

        [Test]
        public void CompareToTest()
        {
            RunOfMine rom = new RunOfMine("2018-01-18", 1, "03S_46_J19", "13_34_F23",
                null, "03S_46_F25", "05N_46_F25", "13_36_J17", "16S_23_G53", "16N_22_F253", "12_34_F25", "13_36_J17");

            RunOfMine rom1 = new RunOfMine("2018-01-18", 1, "03S_46_J19", "13_34_F23",
                null, "03S_46_F25", "05N_46_F25", "13_36_J17", "16S_23_G53", "16N_22_F253", "12_34_F25", "13_36_J17");

            RunOfMine rom3 = new RunOfMine("2018-02-18", 2, "03S_46_J19", "13_34_F23",
                null, "03S_46_F25", "05N_46_F25", "13_36_J17", "16S_23_G53", "16N_22_F253", "12_34_F25", "13_36_J17");

            //Compare objects of equal precedence. 
            Assert.Zero(rom.CompareTo(rom1));

            //Compare instance to object that preceeds in order.
            Assert.Negative(rom.CompareTo(rom3));

            //Compare where instance follows object in predcedence.
            Assert.Positive(rom3.CompareTo(rom1));

        }

        [Test]
        public void CyclePropTest()
        {
            RunOfMine rom = new RunOfMine("2018-01-18", 1, "03S_46_J19", "13_34_F23",
                null, "03S_46_F25", "05N_46_F25", "13_36_J17", "16S_23_G53", "16N_22_F253", "12_34_F25", "13_36_J17");

            Stockpile stock1 = new Stockpile("1", "03S_46_J19");
            Stockpile stock2 = new Stockpile("2", "13_34_F23");
            Stockpile stock4 = new Stockpile("4", "03S_46_F25");
            Stockpile stock5 = new Stockpile("5", "05N_46_F25");
            Stockpile stock6 = new Stockpile("6", "13_36_J17");
            Stockpile stock7 = new Stockpile("7", "16S_23_G53");
            Stockpile stock8 = new Stockpile("8", "16N_22_F253");
            Stockpile stock9 = new Stockpile("9", "12_34_F25");
            Stockpile stock10 = new Stockpile("10", "12_34_F25");

            List<Stockpile> stockpileTemp = new List<Stockpile>();
            stockpileTemp.Add(stock1);
            stockpileTemp.Add(stock2);
            stockpileTemp.Add(stock4);
            stockpileTemp.Add(stock5);
            stockpileTemp.Add(stock6);
            stockpileTemp.Add(stock7);
            stockpileTemp.Add(stock8);
            stockpileTemp.Add(stock9);
            stockpileTemp.Add(stock10);

            List<Stockpile> stockpiles = rom.Stockpiles;

            //Check there is the correct number of stockpiles.
            Assert.AreEqual(stockpiles.Count, 9);

            //Check each stockpile exists.
            foreach (Stockpile s in stockpiles)
            {
                Assert.True(stockpileTemp.Contains(s));
            }
            {

            }

        }
    }

}
