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
    class BlendTest
    {

        [Test]
        public void EqualsTest()
        {
            Blend blend = new Blend("2018-01-18", 1, "03S_46_J19", "13_34_F23",
                "03S_46_F25", "03S_46_F25", "05N_46_F25", "13_36_J17", "16S_23_G53", "16N_22_F253", "12_34_F25", "13_36_J17");

            Blend blend2 = new Blend("2018-01-18", 1, "03S_46_J19", "13_34_F23",
                "03S_46_F25", "03S_46_F25", "05N_46_F25", "13_36_J17", "16S_23_G53", "16N_22_F253", "12_34_F25", "13_36_J17");

            Blend blend3 = new Blend("2018-01-19", 1, "03S_46_J19", "13_34_F23",
                "03S_46_F25", "03S_46_F25", "05N_46_F25", "13_36_J17", "16S_23_G53", "16N_22_F253", "12_34_F25", "13_36_J17");

            Blend blend4 = new Blend("2018-01-18", 2, "03S_46_J19", "13_34_F23",
                "03S_46_F25", "03S_46_F25", "05N_46_F25", "13_36_J17", "16S_23_G53", "16N_22_F253", "12_34_F25", "13_36_J17");

            Blend blend5 = new Blend("2018-01-18", 1, "03S_46_J18", "13_34_F23",
                "03S_46_F25", "03S_46_F25", "05N_46_F25", "13_36_J17", "16S_23_G52", "16N_22_F253", "12_34_F25", "13_36_J17");

            //Test for equal objects.
            Assert.True(blend.Equals(blend2));
            
            //Test for differenet dates.
            Assert.False(blend.Equals(blend3));

            //Test for different priority
            Assert.False(blend.Equals(blend4));

            //Test for some different coals in cycle.
            Assert.False(blend.Equals(blend5));

        }

        [Test]
        public void CompareToTest()
        {
            Blend blend = new Blend("2018-01-18", 1, "03S_46_J19", "13_34_F23",
                "03S_46_F25", "03S_46_F25", "05N_46_F25", "13_36_J17", "16S_23_G53", "16N_22_F253", "12_34_F25", "13_36_J17");

            Blend blend2 = new Blend("2018-01-18", 1, "03S_46_J19", "13_34_F23",
                "03S_46_F25", "03S_46_F25", "05N_46_F25", "13_36_J17", "16S_23_G53", "16N_22_F253", "12_34_F25", "13_36_J17");

            Blend blend3 = new Blend("2018-01-18", 2, "03S_46_J19", "13_34_F23",
                "03S_46_F25", "03S_46_F25", "05N_46_F25", "13_36_J17", "16S_23_G53", "16N_22_F253", "12_34_F25", "13_36_J17");

            //Test for equal precedence.
            Assert.Zero(blend.CompareTo(blend2));

            //Test for this instance being less than the obj.
            Assert.Negative(blend.CompareTo(blend3));

            //Test for this instance being greater than the obj.
            Assert.Positive(blend3.CompareTo(blend));
        }

        [Test]
        public void CycleTest()
        {
            Blend blend = new Blend("2018-01-18", 1, "03S_46_J19", "13_34_F23",
                "03S_46_F25", "03S_46_F25", "05N_46_F25", "13_36_J17", "16S_23_G53", "16N_22_F253", "12_34_F25", "13_36_J17");

            List<string> otherCycle = new List<string>();

            otherCycle.Add("03S_46_J19");
            otherCycle.Add("13_34_F23");
            otherCycle.Add("03S_46_F25");
            otherCycle.Add("03S_46_F25");
            otherCycle.Add("05N_46_F25");
            otherCycle.Add("13_36_J17");
            otherCycle.Add("16S_23_G53");
            otherCycle.Add("16N_22_F253");
            otherCycle.Add("12_34_F25");
            otherCycle.Add("13_36_J17");

            List<string> cycle = blend.Cycle;

            //Test for correct number of elements.
            Assert.True(cycle.Count == 10);

            //Test that each list contains the same elements.
            for (int i = 0; i < cycle.Count; i++)
            {
                Assert.True(cycle[i] == otherCycle[i]);
            }
        }

        [Test]
        public void GetNextCoalTest()
        {
            Blend blend = new Blend("2018-01-18", 1, "03S_46_J19", "13_34_F23",
                "03S_46_F25", "03S_46_F25", "05N_46_F25", "13_36_J17", "16S_23_G53", "16N_22_F253", "12_34_F25", "13_36_J17");

            //Test to get first coal.
            Assert.AreEqual("03S_46_J19", blend.GetNextCoal());
           

            //Test iterating through list.
            Assert.AreEqual("13_34_F23", blend.GetNextCoal());
            Assert.AreEqual("03S_46_F25", blend.GetNextCoal());
            Assert.AreEqual("03S_46_F25", blend.GetNextCoal());
            Assert.AreEqual("05N_46_F25", blend.GetNextCoal());
            Assert.AreEqual("13_36_J17", blend.GetNextCoal());
            Assert.AreEqual("16S_23_G53", blend.GetNextCoal());
            Assert.AreEqual("16N_22_F253", blend.GetNextCoal());
            Assert.AreEqual("12_34_F25", blend.GetNextCoal());
            Assert.AreEqual("13_36_J17", blend.GetNextCoal());

            //Test that list loops to start after finishing list.
            Assert.AreEqual("03S_46_J19", blend.GetNextCoal());

            //Test two more on loop.
            Assert.AreEqual("13_34_F23", blend.GetNextCoal());
            Assert.AreEqual("03S_46_F25", blend.GetNextCoal());

        }

        [Test]
        public void GetCurrentCoalTest()
        {
            Blend blend = new Blend("2018-01-18", 1, "03S_46_J19", "13_34_F23",
                "03S_46_F25", "03S_46_F25", "05N_46_F25", "13_36_J17", "16S_23_G53", "16N_22_F253", "12_34_F25", "13_36_J17");

            //Test for getting coal from start of list.
            Assert.AreEqual("03S_46_J19", blend.GetCurrentCoal());

            blend.GetNextCoal();

            // Test for first coal.  This is due to when using getnextcoal 
            // on a new list it will return the first coal initially opposed to the second.
            Assert.AreEqual("03S_46_J19", blend.GetCurrentCoal());

            blend.GetNextCoal();

            //Check coal is correct after iterating over list first time through.
            Assert.AreEqual("13_34_F23", blend.GetCurrentCoal());
            blend.GetNextCoal();
            Assert.AreEqual("03S_46_F25", blend.GetCurrentCoal());
            blend.GetNextCoal();

            Assert.AreEqual("03S_46_F25", blend.GetCurrentCoal());
            blend.GetNextCoal();
            Assert.AreEqual("05N_46_F25", blend.GetCurrentCoal());
            blend.GetNextCoal();
            Assert.AreEqual("13_36_J17", blend.GetCurrentCoal());
            blend.GetNextCoal();
            Assert.AreEqual("16S_23_G53", blend.GetCurrentCoal());
            blend.GetNextCoal();
            Assert.AreEqual("16N_22_F253", blend.GetCurrentCoal());
            blend.GetNextCoal();
            Assert.AreEqual("12_34_F25", blend.GetCurrentCoal());
            blend.GetNextCoal();
            Assert.AreEqual("13_36_J17", blend.GetCurrentCoal());

            //Check the correct coals are returned once list has looped once.
            blend.GetNextCoal();
            Assert.AreEqual("03S_46_J19", blend.GetCurrentCoal());
            blend.GetNextCoal();
            Assert.AreEqual("13_34_F23", blend.GetCurrentCoal());

        }

    }
}
