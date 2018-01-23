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
    class LoaderTest
    {


        [Test]
        public void LoadRomTruckTest()
        {
            //Create loader.
            Blend blend = new Blend("2018-01-18", 1, "03S_46_J19", "13_34_F23",
                "03S_46_F25", "03S_46_F25", "05N_46_F25", "13_36_J17", "16S_23_G53", "16N_22_F253", "12_34_F25", "13_36_J17");
            TimeSpan maxWaitTime = new TimeSpan(0, 0, 2, 0);
            TimeSpan loadTime = new TimeSpan(0, 0, 5, 0);

            Loader loader = new Loader(blend, maxWaitTime, loadTime);

            //Create coal movements.
            DateTime arrivalTime = new DateTime(2018, 01, 23, 02, 25, 22);
            CoalMovement coalMovement = new CoalMovement("03S_46_J19", "ROM Truck", arrivalTime);

            DateTime arrivalTime2 = new DateTime(2018, 01, 23, 10, 02, 22);
            CoalMovement coalMovement2 = new CoalMovement("13_34_F23", "ROM Truck", arrivalTime2);

            DateTime arrivalTime3 = new DateTime(2018, 05, 24, 02, 32, 22);
            CoalMovement coalMovement3 = new CoalMovement("03S_46_F25", "ROM Truck", arrivalTime3);

            //Load first coal in cycle.
            DateTime startLoadingTime = new DateTime(2018, 01, 23, 02, 20, 22);
            Assert.AreEqual(coalMovement, loader.LoadROMTruck(startLoadingTime));

            //Load following two coals in cycle.

            DateTime startLoadingTime2 = new DateTime(2018, 01, 23, 09, 57, 22);
            Assert.AreEqual(coalMovement2, loader.LoadROMTruck(startLoadingTime2));

            DateTime startLoadingTime3 = new DateTime(2018, 05, 24, 02, 27, 22);
            Assert.AreEqual(coalMovement3, loader.LoadROMTruck(startLoadingTime3));


        }

        [Test]
        public void AllocateCoalMovements()
        {
            //Create loader.
            Blend blend = new Blend("2018-01-18", 1, "03S_46_J19", "13_34_F23",
                "03S_46_F25", "03S_46_F25", "05N_46_F25", "13_36_J17", "16S_23_G53", "16N_22_F253", "12_34_F25", "13_36_J17");
            TimeSpan maxWaitTime = new TimeSpan(0, 0, 2, 0);
            TimeSpan loadTime = new TimeSpan(0, 0, 5, 0);

            Loader loader = new Loader(blend, maxWaitTime, loadTime);

            //Create coal movements.
            DateTime arrivalTime = new DateTime(2018, 01, 23, 02, 25, 22);
            CoalMovement coalMovement = new CoalMovement("03S_46_J19", "ROM Truck", arrivalTime);

            DateTime arrivalTime2 = new DateTime(2018, 01, 23, 02, 31, 22);
            CoalMovement coalMovement2 = new CoalMovement("03S_46_J19", "ROM Truck", arrivalTime2);

            DateTime arrivalTime3 = new DateTime(2018, 01, 23, 02, 25, 22);
            CoalMovement coalMovement3 = new CoalMovement("13_34_F23", "ROM Truck", arrivalTime3);

            DateTime arrivalTime4 = new DateTime(2018, 01, 23, 02, 29, 22);
            CoalMovement coalMovement4 = new CoalMovement("13_34_F23", "ROM Truck", arrivalTime4);

            DateTime arrivalTime5 = new DateTime(2018, 01, 23, 02, 22, 33);
            CoalMovement coalMovement5 = new CoalMovement("13_34_F23", "ROM Truck", arrivalTime5);

            DateTime arrivalTime6 = new DateTime(2018, 01, 23, 02, 26, 18);
            CoalMovement coalMovement6 = new CoalMovement("03S_46_J19", "ROM Truck", arrivalTime);

            DateTime arrivalTime7 = new DateTime(2018, 01, 23, 02, 25, 22);
            CoalMovement coalMovement7 = new CoalMovement("16N_22_F253", "ROM Truck", arrivalTime3);


            DateTime arrivalTime8 = new DateTime(2018, 01, 23, 02, 22, 21);
            CoalMovement coalMovement8 = new CoalMovement("03S_46_J19", "ROM Truck", arrivalTime8);

            //Start time of loading.
            DateTime startTime = new DateTime(2018, 01, 23, 02, 22, 22);

            List<CoalMovement> incomingMovements = new List<CoalMovement>();
            List<CoalMovement> allocatedMovements;

            //Test for empty list of incoming trucks.
            
            allocatedMovements = loader.AllocateCoalMovements(startTime, incomingMovements);

            Assert.AreEqual(0, allocatedMovements.Count);

            //Test for list of single truck that meets requirements.

            incomingMovements.Add(coalMovement);
            loader.ResetBlend();
            allocatedMovements = loader.AllocateCoalMovements(startTime, incomingMovements);

            Assert.AreEqual(1, allocatedMovements.Count);

            Assert.AreEqual(allocatedMovements[0], coalMovement);

            //Test for list of single truck that meets coal requirement but not time.

            incomingMovements.Clear();
            incomingMovements.Add(coalMovement2);
            loader.ResetBlend();

            
            allocatedMovements = loader.AllocateCoalMovements(startTime, incomingMovements);

            Assert.AreEqual(0, allocatedMovements.Count);

            //Test for list of single truck that meets time requirement but not coal. 

            incomingMovements.Clear();
            incomingMovements.Add(coalMovement3);
            loader.ResetBlend();
            allocatedMovements = loader.AllocateCoalMovements(startTime, incomingMovements);

            Assert.AreEqual(0, allocatedMovements.Count);

            //Test for list of two trucks that meet requirements in a row.

            incomingMovements.Clear();
            incomingMovements.Add(coalMovement);
            incomingMovements.Add(coalMovement4);
            loader.ResetBlend();
            allocatedMovements = loader.AllocateCoalMovements(startTime, incomingMovements);

            Assert.AreEqual(2, allocatedMovements.Count);
            Assert.AreEqual(allocatedMovements[0], coalMovement);
            Assert.AreEqual(allocatedMovements[1], coalMovement4);

            //Test for list of two trucks, both trucks meet requirements.  Second required coal arrives first 
            //but falls outside of maximum wait time.

            incomingMovements.Clear();
            incomingMovements.Add(coalMovement5);
            incomingMovements.Add(coalMovement);
            loader.ResetBlend();
            allocatedMovements = loader.AllocateCoalMovements(startTime, incomingMovements);

            Assert.AreEqual(1, allocatedMovements.Count);
            Assert.AreEqual(allocatedMovements[0], coalMovement);

            //Test for list of two trucks, where first truck does not meet blend requirement, second truck does.

            incomingMovements.Clear();
            incomingMovements.Add(coalMovement7);
            incomingMovements.Add(coalMovement6);
            loader.ResetBlend();
            allocatedMovements = loader.AllocateCoalMovements(startTime, incomingMovements);

            Assert.AreEqual(1, allocatedMovements.Count);
            Assert.AreEqual(allocatedMovements[0], coalMovement6);


            //Test for list with multiple trucks.  Some meet requirements others do not.

            //Goes in bin.  New min time is  2018, 01, 23, 02, 23, 22
            DateTime arrivalTime10 = new DateTime(2018, 01, 23, 02, 25, 22);
            CoalMovement coalMovement10 = new CoalMovement("03S_46_J19", "ROM Truck", arrivalTime10);

            //Truck waits 2 minutes.
            DateTime arrivalTime11 = new DateTime(2018, 01, 23, 02, 27, 42);
            CoalMovement coalMovement11 = new CoalMovement("03S_46_F25", "ROM Truck", arrivalTime11);

            //Goes in bin. New min time is 2018, 01, 23, 02, 27, 22
            DateTime arrivalTime12 = new DateTime(2018, 01, 23, 02, 29, 22);
            CoalMovement coalMovement12 = new CoalMovement("13_34_F23", "ROM Truck", arrivalTime12);

            //Goes in bin. New min time is 2018, 01, 23, 02, 28, 35
            DateTime arrivalTime13 = new DateTime(2018, 01, 23, 02, 30, 35);
            CoalMovement coalMovement13 = new CoalMovement("03S_46_F25", "ROM Truck", arrivalTime13);

            //Goes in bin. New min time is 2018, 2018, 01, 23, 02, 31, 12
            DateTime arrivalTime14 = new DateTime(2018, 01, 23, 02, 33, 12);
            CoalMovement coalMovement14 = new CoalMovement("05N_46_F25", "ROM Truck", arrivalTime14);

            //Put on floor not required coal.
            DateTime arrivalTime15 = new DateTime(2018, 01, 23, 02, 35, 35);
            CoalMovement coalMovement15 = new CoalMovement("03S_46_F25", "ROM Truck", arrivalTime15);

            //Put on floor.  Not required coal.
            DateTime arrivalTime16 = new DateTime(2018, 01, 23, 02, 36, 35);
            CoalMovement coalMovement16 = new CoalMovement("16N_22_F253", "ROM Truck", arrivalTime16);

            //put on floor.  Not required coal.
            DateTime arrivalTime17 = new DateTime(2018, 01, 23, 02, 37, 35);
            CoalMovement coalMovement17 = new CoalMovement("16N_22_F253", "ROM Truck", arrivalTime17);

            //Goes in bin.  New min time is (2018, 01, 23, 02, 36, 50
            DateTime arrivalTime18 = new DateTime(2018, 01, 23, 02, 38, 12);
            CoalMovement coalMovement18 = new CoalMovement("13_36_J17", "ROM Truck", arrivalTime18);

            //Goes on floor.  Does not make required time.
            DateTime arrivalTime19 = new DateTime(2018, 01, 23, 02, 43, 50);
            CoalMovement coalMovement19 = new CoalMovement("16S_23_G53", "ROM Truck", arrivalTime19);

            incomingMovements.Clear();
            incomingMovements.Add(coalMovement10);
            incomingMovements.Add(coalMovement11);
            incomingMovements.Add(coalMovement12);
            incomingMovements.Add(coalMovement13);
            incomingMovements.Add(coalMovement14);
            incomingMovements.Add(coalMovement15);
            incomingMovements.Add(coalMovement16);
            incomingMovements.Add(coalMovement17);
            incomingMovements.Add(coalMovement18);
            incomingMovements.Add(coalMovement19);
            loader.ResetBlend();
            allocatedMovements = loader.AllocateCoalMovements(startTime, incomingMovements);

            Assert.AreEqual(allocatedMovements[0], coalMovement10);
            Assert.AreEqual(allocatedMovements[1], coalMovement12);
            Assert.AreEqual(allocatedMovements[2], coalMovement11);
            Assert.AreEqual(allocatedMovements[3], coalMovement13);
            Assert.AreEqual(allocatedMovements[4], coalMovement14);
            Assert.AreEqual(allocatedMovements[5], coalMovement18);

            Assert.AreEqual(6, allocatedMovements.Count);
            

            //Test for coal movement arriving before start time.

            incomingMovements.Clear();
            incomingMovements.Add(coalMovement8);
            loader.ResetBlend();
            

            allocatedMovements = loader.AllocateCoalMovements(startTime, incomingMovements);

            Assert.AreEqual(0, allocatedMovements.Count);

        }


    }
}
