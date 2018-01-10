using System;
using System.Collections.Generic;

namespace ROMLoader.Models
{
    public class RunOfMine : IComparable
    {
        private List<string> cycle;
        private string stockpile1;
        private string stockpile10;
        private string stockpile2;
        private string stockpile3;
        private string stockpile4;
        private string stockpile5;
        private string stockpile6;
        private string stockpile7;
        private string stockpile8;
        private string stockpile9;

        public RunOfMine()
        {
            cycle = new List<string>();
        }

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

        public string Date { get; set; }

        public int Priority { get; set; }

        public string Stockpile1
        {
            get => stockpile1;
            set
            {
                stockpile1 = value;
                AddStockpile("stockpile1", value);
            }
        }

        public string Stockpile2
        {
            get => stockpile2;
            set
            {
                stockpile2 = value;
                AddStockpile("stockpile2", value);
            }
        }

        public string Stockpile3
        {
            get => stockpile3;
            set
            {
                stockpile3 = value;
                AddStockpile("stockpile3", value);
            }
        }

        public string Stockpile4
        {
            get => stockpile4;
            set
            {
                stockpile4 = value;
                AddStockpile("stockpile4", value);
            }
        }

        public string Stockpile5
        {
            get => stockpile5;
            set
            {
                stockpile5 = value;
                AddStockpile("stockpile5", value);
            }
        }

        public string Stockpile6
        {
            get => stockpile6;
            set
            {
                stockpile6 = value;
                AddStockpile("stockpile6", value);
            }
        }

        public string Stockpile7
        {
            get => stockpile7;
            set
            {
                stockpile7 = value;
                AddStockpile("stockpile7", value);
            }
        }

        public string Stockpile8
        {
            get => stockpile8;
            set
            {
                stockpile8 = value;
                AddStockpile("stockpile8", value);
            }
        }

        public string Stockpile9
        {
            get => stockpile9;
            set
            {
                stockpile9 = value;
                AddStockpile("stockpile9", value);
            }
        }

        public string Stockpile10
        {
            get => stockpile10;
            set
            {
                stockpile10 = value;
                AddStockpile("stockpile10", value);
            }
        }

        public List<Stockpile> Stockpiles { get; private set; }

        public List<string> Cycle
        {
            get
            {
                if (cycle == null)
                {
                    cycle = new List<string>();
                }



                return cycle;
            }

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
            var otherROM = (RunOfMine) obj;

            if (Priority > otherROM.Priority)
                return 1;
            if (Priority == otherROM.Priority)
                return 0;
            return -1;
        }

        public void AddStockpile(string stockpileName, string coal)
        {
           
            if (Stockpiles == null)
                Stockpiles = new List<Stockpile>();

            if (coal == null)
                Stockpiles.Add(new Stockpile(stockpileName, "Empty"));
            else
                Stockpiles.Add(new Stockpile(stockpileName, coal));
        }

        /// <summary>
        ///     RunOfMine is considered equal when the dates, priority and all
        ///     stockpiles contain the same values.
        /// </summary>
        /// <param name="obj">The object being checked for equality.</param>
        /// <returns>True if equal.</returns>
        public override bool Equals(object obj)
        {
            var equal = true;

            if (obj == null)
                return false;
            if (obj.GetType() != GetType())
                return false;

            var otherROM = (RunOfMine) obj;

            if (Date == otherROM.Date && Priority == otherROM.Priority)
            {
                var otherStockpileses = otherROM.Stockpiles;
                foreach (var s in Stockpiles)
                    if (!otherStockpileses.Contains(s))
                        equal = false;
            }
            return equal;
        }
    }
}