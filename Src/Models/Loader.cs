using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ROMLoader.Models
{
    public class Loader
    {
        // Represents the sequence coals must be blended.
        private Blend blend;

        // The time it takes to load a truck.
        private TimeSpan loadTime;

        //The maximum time a truck is allowed to wait before dumping coal.
        private TimeSpan maxWaitTime;

        /// <summary>
        /// Initialises a new ROMLoader class.
        /// </summary>
        /// <param name="blendCycle">The list that contains the sequence of how coals are to be loaded.</param>
        /// <param name="maxWaitTime">The maximum time a truck is allowed to wait before dumping coal.</param>
        /// <param name="loadTime">The time it takes to load a truck.</param>
        public Loader(Blend blend, TimeSpan maxWaitTime, TimeSpan loadTime)
        {
            this.blend = blend;
            this.maxWaitTime = maxWaitTime;
            this.loadTime = loadTime;
            
        }

        public TimeSpan LoadTime
        {
            get { return loadTime; }
            set { loadTime = value; }
        }

        public TimeSpan MaxWaitTime
        {
            get { return maxWaitTime; }
            set { maxWaitTime = value; }
        }

        public Blend Blend
        {
            get { return blend; }
        }

        /// <summary>
        /// Determines which trucks that are coming from the pit are to be fed to the ROM bin.  The returned list 
        /// indicates which trucks are to be fed into the bin.  CoalMovements not in this list are dumped at stockpiles.
        /// 
        /// Takes the minimumTime as the starting time of loading and either finds a truck with the required coal 
        /// next in the coal cycle or uses the rom loader to create a new CoalMovement.
        ///
        /// If a CoalMovement with the required coal arrives in less than the loading time but greater than 
        /// the minimum time then it is allocated to the bin.  If a CoalMovement has been found the next coal 
        /// in the blending cycle is inspected, the minimumtime is moved forward to the time of the last truck 
        /// arrival time to preserve ordering of coal loading into the bin and the time range allowed for dumping is
        /// increased proportational to the loadTime for this class.
        /// 
        /// //TODO: clean this up
        /// //If a movement is accepted, the arrival time of that truck becomes the minimum time.
        /// //If the next coal in cycle can be found within the (minimum time - maximum wait time) and minimumtime + loading time
        /// // that coal is also allocated. 
        /// 
        /// </summary>
        /// <param name="startTime">The starting time of loading.</param>
        /// <param name="incomingTrucks">List of coal movements coming from the pit.</param>
        /// <returns>A list of coal movements that are to be allocated to the bin.</returns>
        public List<CoalMovement> AllocateCoalMovements(DateTime startTime, List<CoalMovement> incomingTrucks)
        {
            // List of coal movements to be dumped into the bin.
            List<CoalMovement> allocatedCoalMovements = new List<CoalMovement>();



            incomingTrucks.Sort();

            // Variable to hold the next coal required in blending cycle.
            string requiredCoal = blend.GetNextCoal();

            DateTime maximumTime = startTime.Add(loadTime);
            /*                
            
            maximumTime = maximumTime.Add(maxWaitTime);
            */

            // Variable to keep while loop looping until no CoalMovements can be allocated.
            bool foundMovement = true;

            while (foundMovement)
            {
                // Find coal movements.  
                foundMovement = FindRequiredCoal(startTime, maximumTime ,requiredCoal, incomingTrucks, allocatedCoalMovements);

                //If a truck was found.  Change new time to be the time the truck loaded coal into the bin minus
                // the max wait time.  This allows a truck to wait at the ROM.
                if (allocatedCoalMovements.Count != 0)
                {
                    startTime = allocatedCoalMovements[allocatedCoalMovements.Count - 1].PropDateTime;

                    maximumTime = startTime.Add(loadTime);

                    startTime = startTime.Subtract(maxWaitTime);
                }

                if (foundMovement)
                {
                    requiredCoal = blend.GetNextCoal();
                }
  
            }

            return allocatedCoalMovements;
        }


        /// <summary>
        /// Finds if a CoalMovement is in the incomingCoalMovements falls with inside the minimum and maximum 
        /// time with the required coal.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="requiredCoal"></param>
        /// <param name="incomingCoalMovements"></param>
        /// <param name="resultOfMovements"></param>
        /// <returns></returns>
        private bool FindRequiredCoal(DateTime minimumTime, DateTime maximumTime , string requiredCoal, List<CoalMovement> incomingCoalMovements,
            List<CoalMovement> resultOfMovements)
        {

                foreach (CoalMovement coalMovement in incomingCoalMovements)
                {

                   if (coalMovement.Coal == requiredCoal && coalMovement.PropDateTime <= maximumTime &&
                        coalMovement.PropDateTime >= minimumTime && coalMovement.Coal == requiredCoal)
                    {
                        // Add the coal movement to the results of movements list.
                        resultOfMovements.Add(coalMovement);

                        // Remove coal from movements - Cannt be used again.
                        incomingCoalMovements.Remove(coalMovement);

                        return true;
                    }
                }
            
            //Coal required not found.  Exit out and load coal with loader.
            return false;
        }

        /// <summary>
        /// Creates a new coal movement with the next coal in the blend.
        /// </summary>
        /// <param name="requiredCoal">The coal in the the coal movement.</param>
        /// <param name="time">The time the truck started to be loaded.</param>
        /// <returns>A coal movement with the required next coal in the blend and 
        /// the time it is expected to be dumped into the bin.</returns>
        public CoalMovement LoadROMTruck(DateTime time)
        {
            CoalMovement romMovement = new CoalMovement(blend.GetCurrentCoal(), "ROM Truck", time.Add(loadTime));
            return romMovement;
        }


        public void ResetBlend()
        {
            blend.ResetCycle();
        }

        public int CycleIndex()
        {
            return blend.GetCoalIndex();
        }


    }
}