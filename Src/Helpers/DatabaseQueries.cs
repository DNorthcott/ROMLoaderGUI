using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ROMLoader.Models;
using SQLite;

namespace ROMLoader.Src.Helpers
{
    public static class DatabaseQueries
    {


        public static async Task<List<Blend>> GetBlendsAsync(DateTime date, SQLiteAsyncConnection database)
        {
            string sql = "SELECT * FROM blend WHERE DateOfBlend = " + "'" + date.ToString("yyyy-MM-dd") + "'" +
                "and Priority = (select MAX(Priority)  FROM blend WHERE DateOfBlend = '" + date.ToString("yyyy-MM-dd")
                + "')";

            List<Blend> blend = await database.QueryAsync<Blend>(sql);

            return blend;
        }

        public static async Task<List<RunOfMine>> GetRunOfMineAsync(DateTime date, SQLiteAsyncConnection database)
        {
            string sql = "SELECT * FROM RunOfMine WHERE date = " + "'" + date.ToString("yyyy-MM-dd") + "'" +
                         "and Priority = (select MAX(Priority)  FROM RunOfMine WHERE date = '" + date.ToString("yyyy-MM-dd")
                         + "')";

            List<RunOfMine> runOfMine = await database.QueryAsync<RunOfMine>(sql);
            

            return runOfMine;
        }

        //Updates the given coal movement to be fed into the bin.  Feed becomes true (1).
        public static async Task UpdateCoalMovements(CoalMovement coalMovement, SQLiteAsyncConnection database)
        {
            string sql = "UPDATE CoalMovement" +
                         " SET Feed = 1" +
                         " WHERE Coal = '" + coalMovement.Coal + "' AND Truck = '" + coalMovement.Truck + 
                         "' AND DateTimeArrival = '" + coalMovement.PropDateTime.ToString("yyyy-MM-dd HH:mm:ss") +
                         "'";

            await database.QueryAsync<CoalMovement>(sql);

        }

        // Gets all the coal movements for a set time period.
        public static async Task<List<CoalMovement>> GetCoalMovements(DateTime time, int minutes, 
            SQLiteAsyncConnection database)
        {
            string currentTime = time.ToString("yyyy-MM-dd HH:mm:ss");
            time = time.AddMinutes(minutes);
            string futureTime = time.ToString("yyyy-MM-dd HH:mm:ss");

            //Put '' around times for sql strings.
            currentTime = "'" + currentTime + "'";
            futureTime = "'" + futureTime + "'";

            string sql = "SELECT * FROM coalMovement " +
                         "where DateTimeArrival > " + currentTime +
                         "and DateTimeArrival <= " + futureTime + " and Feed = 0";

            List<CoalMovement> coalMovements = await database.QueryAsync<CoalMovement>(sql);
            coalMovements.Sort();

            return coalMovements;
        }


    }
}
