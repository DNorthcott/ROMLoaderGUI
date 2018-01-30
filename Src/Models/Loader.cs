using System;
using System.Collections.Generic;

namespace ROMLoader.Models
{
    /// <summary>
    ///     The loader represents the truck that loads coal at the ROM.  The loader class is responsible
    ///     for allocating incoming trucks based on a blend cycle based on a load time and the maximum
    ///     time a truck is allowed to wait before dumping coal.
    /// </summary>
    public class Loader
    {
        /// <summary>
        ///     Creates a new Loader object.
        /// </summary>
        /// <param name="blendCycle">The list that contains the sequence of how coals are to be loaded.</param>
        /// <param name="maxWaitTime">The maximum time a truck is allowed to wait before dumping coal.</param>
        /// <param name="loadTime">The time it takes to load a truck.</param>
        public Loader(Blend blend, TimeSpan maxWaitTime, TimeSpan loadTime)
        {
            Blend = blend;
            MaxWaitTime = maxWaitTime;
            LoadTime = loadTime;

        }

        /// <summary>
        ///     Allocates coal movements to either feed into the bin or to stockpile.
        ///     The trucks are allocated based on their arrival times and the loaders load time and trucks maximum wait
        ///     time.
        ///     The start time indicates the time when the loader will start loading.  If a coalmovement with the required
        ///     coal will arrive between the start time and (start time + load time) that movement is allocated.
        ///     After a coal movement has been allocated, the next required coal is searched for.  It must now fall within
        ///     the previous trucks (arrival time - maximum wait time) and (arrival time + load time).
        ///     This method attempts to have a coal movement fed into the bin in intervals less than the load time.
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
            string requiredCoal = Blend.GetNextCoal();

            DateTime maximumTime = startTime.Add(LoadTime);

            // Variable to keep while loop looping until no more CoalMovements can be allocated.
            bool foundMovement = true;

            while (foundMovement)
            {
                // Find coal movements.  
                foundMovement = FindRequiredCoal(startTime, maximumTime, requiredCoal,
                    incomingTrucks, allocatedCoalMovements);

                /*If a coalmovement was found,  change start time to be the time the truck loaded coal into the bin 
                  minus the max wait time.  This allows a truck to wait at the ROM.
                  Update maximumtime to be the coalmovements start time + load time.
                */
                if (allocatedCoalMovements.Count != 0)
                {
                    startTime = allocatedCoalMovements[allocatedCoalMovements.Count - 1].PropDateTime;

                    maximumTime = startTime.Add(LoadTime);

                    startTime = startTime.Subtract(MaxWaitTime);
                }

                if (foundMovement)
                {
                    requiredCoal = Blend.GetNextCoal();
                }

            }

            return allocatedCoalMovements;
        }


        /// <summary>
        ///     Finds if a CoalMovement is in the incomingCoalMovements falls with inside the minimum and maximum
        ///     time with the required coal.
        /// </summary>
        /// <param name="maximumTime">The maximum time in which to find a coal movement.</param>
        /// <param name="requiredCoal">The type of coal required.</param>
        /// <param name="incomingCoalMovements">A list of coalmovements.</param>
        /// <param name="resultOfMovements">
        ///     A list of allocated trucks, this is used as a reference type
        ///     to add allocated trucks to it.
        /// </param>
        /// <param name="minimumTime">The minimum time in which to find a coal movement.</param>
        /// <returns>True if found, false if not found.</returns>
        private bool FindRequiredCoal(DateTime minimumTime, DateTime maximumTime, string requiredCoal,
            List<CoalMovement> incomingCoalMovements, List<CoalMovement> resultOfMovements)
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
        ///     Creates a new coal movement with the next coal in the blend.
        /// </summary>
        /// <param name="requiredCoal">The required coal to be loaded.</param>
        /// <param name="time">The time the truck started to be loaded.</param>
        /// <returns>
        ///     A coal movement with the required next coal in the blend and
        ///     the time it is expected to be dumped into the bin.
        /// </returns>
        public CoalMovement LoadROMTruck(DateTime time)
        {
            CoalMovement romMovement = new CoalMovement(Blend.GetCurrentCoal(), "ROM Truck", time.Add(LoadTime));
            return romMovement;
        }

        /// <summary>
        ///     Resets the blend to the starting point.
        /// </summary>
        public void ResetBlend()
        {
            Blend.ResetCycle();
        }

        /// <summary>
        ///     Returns the index in which the blend is currently at.
        /// </summary>
        public int CycleIndex()
        {
            return Blend.GetCoalIndex();
        }

        //-------------------------------------
        // Properties
        //-------------------------------------

        /// <summary>
        /// Represents the time required to load coal into a truck.
        /// This also represents time when allocated coals, the maximum interval
        /// between incoming trucks.
        /// </summary>
        public TimeSpan LoadTime { get; set; }


        /// <summary>
        /// The maximum time in minutes a truck is allowed to wait before dumping coal.
        /// </summary>
        public TimeSpan MaxWaitTime { get; set; }

        /// <summary>
        /// The blend that the loader uses to determine the sequence of coals to be loaded/allocated.
        /// </summary>
        public Blend Blend { get; }
    }
}