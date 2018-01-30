using System;

namespace ROMLoader.Models
{
    /// <summary>
    ///     <p>
    ///         A coal movement is the movement of coal from a coal source to the ROM.
    ///         It contains the name of the truck, type of coal
    ///         and tonnage.
    ///     </p>
    /// </summary>
    public class CoalMovement : IComparable
    {
        private string dateTimeArrival;
        private string endLocation;

        /// <summary>
        ///     Creates a new CoalEntry class.
        /// </summary>
        public CoalMovement()
        {
        }

        /// <summary>
        ///     Used for unit testing.
        /// </summary>
        /// <param name="coal"></param>
        /// <param name="truck"></param>
        /// <param name="dateTimeArrival"></param>
        public CoalMovement(string coal, string truck, string dateTimeArrival)
        {
            Coal = coal;
            Truck = truck;
            DateTimeArrival = dateTimeArrival;
        }

        /// <summary>
        ///     Used for unit testing.
        /// </summary>
        /// <param name="coal"></param>
        /// <param name="truck"></param>
        /// <param name="dateTimeArrival"></param>
        public CoalMovement(string coal, string truck, DateTime dateTimeArrival)
        {
            Coal = coal;
            Truck = truck;
            PropDateTime = dateTimeArrival;
        }

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
            CoalMovement otherCoalMovement = (CoalMovement) obj;

            return PropDateTime.CompareTo(otherCoalMovement.PropDateTime);
        }

        /// <summary>
        ///     Coal movements are considered equal when they contain the same coal, same datetime
        ///     and same truck name.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj.GetType() != GetType())
                return false;

            CoalMovement otherMovement = (CoalMovement) obj;

            if (Coal == otherMovement.Coal && Truck == otherMovement.Truck &&
                PropDateTime.Equals(otherMovement.PropDateTime))
                return true;
            return false;
        }


        //-----------------------------------
        //Properties.
        //-----------------------------------

        public string Truck { get; set; }

        public string Coal { get; set; }

        /// <summary>
        ///     Converts the SQLite boolean int values to C# boolean.
        /// </summary>
        public int Feed
        {
            set
            {
                if (value == 1)
                {
                    FeedIntoBin = true;
                }
                else
                {
                    FeedIntoBin = false;
                }
            }
        }

        public bool FeedIntoBin { get; private set; }

        /// <summary>
        ///     Arrival time in string format.
        ///     Property for DateTimeArrival.  Also sets the PropDateTime object.
        /// </summary>
        public string DateTimeArrival
        {
            get => dateTimeArrival;
            set
            {
                dateTimeArrival = value;
                PropDateTime = Convert.ToDateTime(value);
            }
        }

        /// <summary>
        ///     Arrival time in DateTime object format.
        /// </summary>
        public DateTime PropDateTime { get; private set; }
    }
}