using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ROMLoader.Annotations;
using ROMLoader.Models;

namespace ROMLoader.tests
{
    class CoalMovementTest
    {
        
        [Test]
        public void EqualsTest()
        {
            CoalMovement coalMovement = new CoalMovement("05N_46_F25", "Truck1", "2017-12-12 00:31:45");
            CoalMovement coalMovement1 = new CoalMovement("05N_46_F25", "Truck1", "2017-12-12 00:31:45");

            CoalMovement coalMovement2 = new CoalMovement("05N_45_F25", "Truck1", "2017-12-12 00:31:45");
            CoalMovement coalMovement3 = new CoalMovement("05N_46_F25", "Truck2", "2017-12-12 00:31:45");
            CoalMovement coalMovement4 = new CoalMovement("05N_46_F25", "Truck1", "2017-12-12 00:32:45");
            CoalMovement coalMovement5 = new CoalMovement("05N_45_F25", "Truck2", "2018-12-12 00:31:45");
            //Test for equal objects.
            Assert.True(coalMovement.Equals(coalMovement1));

            //Test for when coal is different, other variables equal.
            Assert.False(coalMovement.Equals(coalMovement2));

            //Test for different truck, other variables equal.
            Assert.False(coalMovement.Equals(coalMovement3));

            //Test for different arrival time, other variables equal.
            Assert.False(coalMovement.Equals(coalMovement4));

            //Test for all variables different.
            Assert.False(coalMovement.Equals(coalMovement5));


        }

        [Test]
        public void CompareToTest()
        {
            CoalMovement coalMovement = new CoalMovement("05N_46_F25", "Truck1", "2017-12-12 00:31:45");
            CoalMovement coalMovement1 = new CoalMovement("05N_46_F25", "Truck1", "2017-12-12 00:31:45");

            CoalMovement coalMovement2 = new CoalMovement("05N_46_F25", "Truck1", "2017-12-12 00:31:32");

            CoalMovement coalMovement3 = new CoalMovement("05N_46_F25", "Truck1", "2017-12-12 00:32:45");

            CoalMovement coalMovement4 = new CoalMovement("05N_46_F25", "Truck1", "2017-12-12 01:31:45");

            CoalMovement coalMovement5 = new CoalMovement("05N_46_F25", "Truck1", "2017-12-13 00:31:45");

            CoalMovement coalMovement6 = new CoalMovement("05N_46_F25", "Truck1", "2017-11-12 00:31:45");

            CoalMovement coalMovement7 = new CoalMovement("05N_46_F25", "Truck1", "2018-11-12 00:31:45");

            // Test for equal sorting.
            Assert.Zero(coalMovement.CompareTo(coalMovement1));

            // Test for where this instance is less than the object in question.
            Assert.Negative(coalMovement2.CompareTo(coalMovement));
            Assert.Negative(coalMovement2.CompareTo(coalMovement3));
            Assert.Negative(coalMovement2.CompareTo(coalMovement4));
            Assert.Negative(coalMovement2.CompareTo(coalMovement5));
            Assert.Negative(coalMovement6.CompareTo(coalMovement2));
            Assert.Negative(coalMovement2.CompareTo(coalMovement7));

            // Test for where this instance is greater than the object in question.
            Assert.Positive(coalMovement.CompareTo(coalMovement2));
            Assert.Positive(coalMovement3.CompareTo(coalMovement2));
            Assert.Positive(coalMovement4.CompareTo(coalMovement2));
            Assert.Positive(coalMovement5.CompareTo(coalMovement2));
            Assert.Positive(coalMovement2.CompareTo(coalMovement6));
            Assert.Positive(coalMovement7.CompareTo(coalMovement2));

        }



    }
}
