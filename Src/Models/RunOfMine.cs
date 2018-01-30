using System;
using System.Collections.Generic;

namespace ROMLoader.Models
{
    /// <summary>
    ///     RunOfMine stores a list of stockpiles stored at the ROM.
    /// </summary>
    public class RunOfMine : IComparable
    {
        private DateTime date;
        Dictionary<string, string> stockpiles =
            new Dictionary<string, string>();

        /// <summary>
        ///     Creates a new ROM object.
        /// </summary>
        public RunOfMine()
        {
        }

        /// <summary>
        ///     Used for testing.
        /// </summary>
        public RunOfMine(string date, int priority, string stockpile1, string stockpile2, string stockpile3,
            string stockpile4, string stockpile5, string stockpile6, string stockpile7, string stockpile8
            , string stockpile9, string stockpile10)
        {
            Date = date;
            Priority = priority;
            Stockpile1 = stockpile1;
            Stockpile2 = stockpile2;
            Stockpile3 = stockpile3;
            Stockpile4 = stockpile4;
            Stockpile5 = stockpile5;
            Stockpile6 = stockpile6;
            Stockpile7 = stockpile7;
            Stockpile8 = stockpile8;
            Stockpile9 = stockpile9;
            Stockpile10 = stockpile10;
        }

        /// <summary>
        ///     Compares the RunOfMine object.  A higher priority integer
        ///     is considered greater than the arguement.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>
        ///     A positive number if the arguement is less than this objects
        ///     priority.  0 if equal and -1 if less than.
        /// </returns>
        public int CompareTo(object obj)
        {
            RunOfMine otherROM = (RunOfMine) obj;

            if (Priority > otherROM.Priority)
                return 1;
            if (Priority == otherROM.Priority)
                return 0;
            return -1;
        }

        /// <summary>
        ///     Adds a stockpile to the list of stockpiles.
        /// </summary>
        /// <param name="stockpileName">The name of the stockpile.</param>
        /// <param name="coal">The coal to go into the stockpile.</param>
        private void AddStockpile(int stockpileNumber, string coal)
        {

            if (Stockpiles == null)
                Stockpiles = new List<Stockpile>();

            if (coal == null)
                Stockpiles.Add(new Stockpile(stockpileNumber, "Empty"));
            else
                Stockpiles.Add(new Stockpile(stockpileNumber, coal));
        }

        /// <summary>
        ///     RunOfMine is considered equal when the dates, priority and all
        ///     stockpiles contain the same values.
        /// </summary>
        /// <param name="obj">The object being checked for equality.</param>
        /// <returns>True if equal.</returns>
        public override bool Equals(object obj)
        {
            bool equal = true;

            if (obj == null)
                return false;
            if (obj.GetType() != GetType())
                return false;

            RunOfMine otherROM = (RunOfMine) obj;

            if (Date == otherROM.Date && Priority == otherROM.Priority)
            {
                List<Stockpile> otherStockpileses = otherROM.Stockpiles;
                foreach (var s in Stockpiles)
                    if (!otherStockpileses.Contains(s))
                        equal = false;
            }
            else
            {
                return false;
            }
            return equal;
        }

        //--------------------------------------------------
        // Properties
        //--------------------------------------------------

        public string Date
        {
            get => date.ToString();
            set => date = Convert.ToDateTime(value);
        }

        public int Priority { get; set; }

        public string Stockpile1
        {
            get => stockpiles["stockpile1"];
            set
            {
                stockpiles["stockpile1"] = value;
                AddStockpile(1, value);
            }
        }

        public string Stockpile2
        {
            get => stockpiles["stockpile2"];
            set
            {
                stockpiles["stockpile2"] = value;
                AddStockpile(1, value);
            }
        }

        public string Stockpile3
        {
            get => stockpiles["stockpile3"];
            set
            {
                stockpiles["stockpile3"] = value;
                AddStockpile(1, value);
            }
        }

        public string Stockpile4
        {
            get => stockpiles["stockpile4"];
            set
            {
                stockpiles["stockpile4"] = value;
                AddStockpile(1, value);
            }
        }

        public string Stockpile5
        {
            get => stockpiles["stockpile5"];
            set
            {
                stockpiles["stockpile5"] = value;
                AddStockpile(1, value);
            }
        }

        public string Stockpile6
        {
            get => stockpiles["stockpile6"];
            set
            {
                stockpiles["stockpile6"] = value;
                AddStockpile(1, value);
            }
        }

        public string Stockpile7
        {
            get => stockpiles["stockpile7"];
            set
            {
                stockpiles["stockpile7"] = value;
                AddStockpile(1, value);
            }
        }

        public string Stockpile8
        {
            get => stockpiles["stockpile8"];
            set
            {
                stockpiles["stockpile8"] = value;
                AddStockpile(1, value);
            }
        }

        public string Stockpile9
        {
            get => stockpiles["stockpile9"];
            set
            {
                stockpiles["stockpile9"] = value;
                AddStockpile(1, value);
            }
        }

        public string Stockpile10
        {
            get => stockpiles["stockpile10"];
            set
            {
                stockpiles["stockpile10"] = value;
                AddStockpile(1, value);
            }
        }

        public List<Stockpile> Stockpiles { get; private set; }
    }
}