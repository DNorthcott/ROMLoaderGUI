using System;

namespace ROMLoader.Models
{
    /// <summary>
    ///     Models a stockpile, contains a stockpile number and the type of coal.
    /// </summary>
    public class Stockpile : IComparable
    {
        /// <summary>
        ///     Constructs a new stockpile.
        /// </summary>
        /// <param name="number">The number of the stockpile.</param>
        /// <param name="coal">The type of coal the stockpile contains.</param>
        public Stockpile(int number, string coal)
        {
            Coal = coal;
            StockPileNumber = number;
        }

        /// <summary>
        ///     Compares stockpiles, sorts based on stockpile numbers where
        ///     the lower the stockpile number comes first compared a higher number.
        public int CompareTo(object obj)
        {
            Stockpile otherStockpile = (Stockpile) obj;

            if (StockPileNumber > otherStockpile.StockPileNumber)
            {
                return 1;
            }
            else if (StockPileNumber == otherStockpile.StockPileNumber)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        ///     Stockpiles are considered equal when they contain the same coal and stockpile number.
        /// </summary>
        public override bool Equals(object obj)
        {

            if (obj == null)
            {
                return false;
            }
            else if (obj.GetType() != GetType())
            {
                return false;
            }

            Stockpile otherStockpile = (Stockpile) obj;

            if (Coal == otherStockpile.Coal &&
                StockPileNumber == otherStockpile.StockPileNumber)
            {
                return true;
            }
            return false;
        }

        //------------------------------------------
        // Properties
        //------------------------------------------


        public int StockPileNumber { get; }

        public string Coal { get; }

        public override string ToString()
        {
            return "Stockpile " + StockPileNumber + " : " + Coal;
        }
    }
}
