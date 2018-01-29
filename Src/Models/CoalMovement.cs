using System;
using System.ComponentModel;
using System.Windows;

namespace ROMLoader.Models
{
    /// <summary>
    ///     <p>
    ///         A coal movement is the movement of coal from pit to ROM.  It contains the name of the truck, type of coal
    ///         and tonnage.
    ///     </p>
    /// </summary>
    public class CoalMovement : IComparable
    {
        private string dateTimeArrival;
        private string endLocation;
        private bool feed;

        /// <summary>
        ///     Creates a new CoalEntry class.
        /// </summary>
        /// <param name="coal">The type of coal this wrapper contains.</param>
        /// <param name="truckName">The name of the truck that is moving the coal.</param>
        /// <param name="arrivalTime">The expected time of arrival of the truck in the ROM.</param>
        public CoalMovement()
        {
        }

        public CoalMovement(string coal, string truck, string dateTimeArrival)
        {
            Coal = coal;
            Truck = truck;
            DateTimeArrival = dateTimeArrival;
        }

        public CoalMovement(string coal, string truck, DateTime dateTimeArrival)
        {
            Coal = coal;
            Truck = truck;
            PropDateTime = dateTimeArrival;
        }

        public string Truck { get; set; }

        public string Coal { get; set; }

        /// <summary>
        /// Determines if the coal is fed into the bin or stockpiled.
        /// True - Direct feed.
        /// False - Stockpiled.
        /// </summary>
        public int Feed
        {
            set
            {
                if (value == 1)
                {
                    feed = true;
                }
                else
                {
                    feed = false;
                }
            }
        }

        public bool FeedIntoBin
        {
            get { return feed; }
        }

        public string DateTimeArrival
        {
            get => dateTimeArrival;
            set
            {
                dateTimeArrival = value;
                PropDateTime = Convert.ToDateTime(value);
            }
        }

        public DateTime PropDateTime { get; private set; }

        /// <summary>
        ///     Compares the trucks based on arrival time.  Eg earlier times are first and later times second.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>
        ///     Less than 0 if the current instance precededs the object specified, same if 0 and greater than 1 if greater
        ///     than.
        /// </returns>
        public int CompareTo(object obj)
        {
            var otherCoalMovement = (CoalMovement) obj;

            return PropDateTime.CompareTo(otherCoalMovement.PropDateTime);
        }

        /// <summary>
        /// Coal movements are considered equal when they contain the same coal, same datetime 
        /// and same truck name.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != GetType())
                return false;

            var otherMovement = (CoalMovement) obj;

            if (Coal == otherMovement.Coal && Truck == otherMovement.Truck &&
                PropDateTime.Equals(otherMovement.PropDateTime))
                return true;
            return false;
        }


    }
}