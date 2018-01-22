using System;

namespace ROMLoader.Models
{
    public class Stockpile : IComparable
    {

  
        private string coal;
        private int stockPileNumber;

        public Stockpile(string name, string coal)
        {
            
            this.coal = coal;

            stockPileNumber = Int32.Parse(name);
        }

        public int StockPileNumber
        {
            get { return stockPileNumber; }
        }

        public string Coal
        {
            get { return coal; }
        }

        public override string ToString()
        {
            return "Stockpile " + StockPileNumber + " : " + Coal;
        }

        public int CompareTo(object obj)
        {
            Stockpile otherStockpile = (Stockpile)obj;

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

            if (coal == otherStockpile.Coal &&
                stockPileNumber == otherStockpile.StockPileNumber)
            {
                return true;
            }
            return false;
        }
    }
}
