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
        public void GetCurrentCoalTest()
        {
            Blend blend = new Blend("2018-01-18", 1, "03S_46_J19", "13_34_F23",
                "03S_46_F25", "03S_46_F25", "05N_46_F25", "13_36_J17", "16S_23_G53", "16N_22_F253", "12_34_F25", "13_36_J17");
        }

        [Test]
        public void LoadRomTruckTest()
        {
            
        }

        [Test]
        public void AllocateCoalMovements()
        {
            
        }

    }
}
