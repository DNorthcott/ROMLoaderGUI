using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ROMLoader.Models;
using SQLite;

namespace ROMLoader.Helpers
{
    /// <summary>
    /// A static class that executes SQL statements to the SQLite database.
    /// 
    /// Side note: In the database a single day can have multiple blends or ROM's, 
    /// the blend/ROM with the highest priority number is the blend that must be used.
    /// The reasoning behind this is that a blend/ROM can change midshift if an issue occurs.
    /// </summary>
    public static class DatabaseQueries
    {
        /// <summary>
        /// Queries the SQLite database and returns a list that contains a single blend.
        /// The blend returned has the highest priority for that given day.
        /// </summary>
        /// <param name="date">The date of the required blends.</param>
        /// <param name="database">The connection to SQLite DB.</param>
        /// <returns>A List containing a single blend.</returns>
        public static async Task<List<Blend>> GetBlendsAsync(DateTime date, SQLiteAsyncConnection database)
        {
            string sql = "SELECT * FROM blend WHERE DateOfBlend = " + "'" + date.ToString("yyyy-MM-dd") + "'" +
                         "and Priority = (select MAX(Priority)  FROM blend WHERE DateOfBlend = '" +
                         date.ToString("yyyy-MM-dd")
                         + "')";

            List<Blend> blend = await database.QueryAsync<Blend>(sql);

            return blend;
        }

        /// <summary>
        /// Queries the SQLite database and returns a list that contains a single RunOfMine object.
        /// The ROM returned has the highest priority for that given day.
        /// </summary>
        /// <param name="date">The date of the required ROM.</param>
        /// <param name="database">The connection to SQLite DB.</param>
        /// <returns>A list containing a single ROM object.</returns>
        public static async Task<List<RunOfMine>> GetRunOfMineAsync(DateTime date, SQLiteAsyncConnection database)
        {
            string sql = "SELECT * FROM RunOfMine WHERE date = " + "'" + date.ToString("yyyy-MM-dd") + "'" +
                         "and Priority = (select MAX(Priority)  FROM RunOfMine WHERE date = '" +
                         date.ToString("yyyy-MM-dd")
                         + "')";

            List<RunOfMine> runOfMine = await database.QueryAsync<RunOfMine>(sql);

            return runOfMine;
        }

        //Updates the given coal movement to be fed into the bin.  Feed becomes true (1).
        /// <summary>
        /// Updates the coal movement in the database.  The coal movement feed will be changed
        /// to true (1).  This stops a single coal movement being allocated twice.
        /// </summary>
        /// <param name="coalMovement">The coal movement to be updated.</param>
        /// <param name="database">The connection to SQLite DB.</param>
        /// <returns>Returns Task (no return)</returns>
        public static async Task UpdateCoalMovements(CoalMovement coalMovement, SQLiteAsyncConnection database)
        {
            string sql = "UPDATE CoalMovement" +
                         " SET Feed = 1" +
                         " WHERE Coal = '" + coalMovement.Coal + "' AND Truck = '" + coalMovement.Truck +
                         "' AND DateTimeArrival = '" + coalMovement.PropDateTime.ToString("yyyy-MM-dd HH:mm:ss") +
                         "'";

            await database.QueryAsync<CoalMovement>(sql);

        }

        /// <summary>
        /// Queries the datbase in question and returns coal movements between time (starting point) and 
        /// a set number minutes after this starting point.  Eg.  if DateTime is 11:00am on the 30/01/2018
        /// and the minutes is 30.  It will return the coalmovements on the 30/01/2018 between 11.00 and 11.30am.
        /// </summary>
        /// <param name="time">The start time of the required movements.</param>
        /// <param name="minutes">The number of minutes after the start time coal movements are required.</param>
        /// <param name="database">The connection to SQLite DB.</param>
        /// <returns>A list of coalmovements that fall in the required time frame.</returns>
        public static async Task<List<CoalMovement>> GetCoalMovements(DateTime time, int minutes,
            SQLiteAsyncConnection database)
        {
            string currentTime = time.ToString("yyyy-MM-dd HH:mm:ss");
            time = time.AddMinutes(minutes);
            string maximumTime = time.ToString("yyyy-MM-dd HH:mm:ss");

            //Put '' around times for sql strings.
            currentTime = "'" + currentTime + "'";
            maximumTime = "'" + maximumTime + "'";

            string sql = "SELECT * FROM coalMovement " +
                         "where DateTimeArrival > " + currentTime +
                         "and DateTimeArrival <= " + maximumTime + " and Feed = 0";

            List<CoalMovement> coalMovements = await database.QueryAsync<CoalMovement>(sql);
            coalMovements.Sort();

            return coalMovements;
        }
    }
}
