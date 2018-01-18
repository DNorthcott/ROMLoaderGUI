using System;
using System.Collections.Generic;

namespace ROMLoader.Models
{
    public class Loader
    {
        // Represents the sequence coals must be blended.
        private readonly List<string> blendCycle;
        // Points to the index of the current coal in the blend cycle.
        private int index;
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
        public Loader(List<string> blendCycle, TimeSpan maxWaitTime, TimeSpan loadTime)
        {
            this.blendCycle = blendCycle;
            index = 0;
            this.maxWaitTime = maxWaitTime;
            this.loadTime = loadTime;
            
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
        /// </summary>
        /// <param name="minimumTime">The starting time of loading.</param>
        /// <param name="IncomingTrucks">List of coal movements coming from the pit.</param>
        /// <returns>A list of coal movements that are to be allocated to the bin.</returns>
        public List<CoalMovement> AllocateCoalMovements(DateTime minimumTime, List<CoalMovement> IncomingTrucks)
        {
            // List of coal movements to be dumped into the bin.
            List<CoalMovement> allocatedCoalMovements = new List<CoalMovement>();

            // Variable to keep while loop looping until no CoalMovements can be allocated.
            bool foundMovement = true;

            // Variable to hold the next coal required in blending cycle.
            string requiredCoal = ""; 

            while (foundMovement)
            {
                requiredCoal = GetNextCoal();

                // Find coal movements.  
                foundMovement = FindRequireCoal(minimumTime, requiredCoal, IncomingTrucks, allocatedCoalMovements);

                //If a truck was found.  Change new time to be the time the truck loaded coal into the bin.
                if (allocatedCoalMovements.Count != 0)
                {
                    minimumTime = allocatedCoalMovements[allocatedCoalMovements.Count - 1].PropDateTime
                        .Subtract(maxWaitTime);

                }
            }


            // TODO: Review timings for ROM truck.  This should be fine as is, due to case of no trucks found.
            
            // Subtract load time.  May make changes to time here later to load rom truck earlier.
            minimumTime = minimumTime.Subtract(loadTime);
            allocatedCoalMovements.Add(LoadROMTruck(requiredCoal, minimumTime));

            return allocatedCoalMovements;
        }

        /// <summary>
        /// Returns a new coal movement with the ROM truck labeled as the truck.
        /// </summary>
        /// <param name="requiredCoal">The coal in the the coal movement.</param>
        /// <param name="time">ETA of the rom truck to be loaded.</param>
        /// <returns></returns>
        private CoalMovement LoadROMTruck(string requiredCoal, DateTime time)
        {
            CoalMovement romMovement = new CoalMovement(requiredCoal, "ROM Truck", time);
            return romMovement;
        }

        /// <summary>
        /// Finds if a CoalMovement is in the incomingCoalMovements falls with inside the minimum and maximum 
        /// time with the required coal.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="requiredCoal"></param>
        /// <param name="incomgCoalMovements"></param>
        /// <param name="resultOfMovements"></param>
        /// <returns></returns>
        private bool FindRequireCoal(DateTime time, string requiredCoal, List<CoalMovement> incomingCoalMovements,
            List<CoalMovement> resultOfMovements)
        {

                foreach (CoalMovement coalMovement in incomingCoalMovements)
                {
                   //Are the coal movements sorted by time? 
                     DateTime maximumTime = time.Add(loadTime);
                    
                    if (coalMovement.Coal.Equals(requiredCoal) && coalMovement.PropDateTime < maximumTime &&
                        coalMovement.PropDateTime > time && coalMovement.Coal == requiredCoal)

                    // movement.Coal.Equals(coal)
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
        // TODO: breaks at end of cycle.
        private string GetNextCoal()
        {
            
            string coal = blendCycle[(index) % blendCycle.Count];
            index++;
            return coal;
        }

        /// <summary>
        /// This may now be redundant.
        /// </summary>
        /// <param name="coal"></param>
        /// <param name="movements"></param>
        /// <returns></returns>
        private bool MovementsContainRequiredCoal(string coal, List<CoalMovement> movements)
        {
            foreach (CoalMovement movement in movements)
            {
                if (movement.Coal.Equals(coal))
                    return true;
            }
            return false;
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
    }
}