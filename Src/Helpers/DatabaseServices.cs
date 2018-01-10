using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace ROMLoader.Models
{
    public class DatabaseServices
    {
        private readonly SQLiteAsyncConnection database;

        public DatabaseServices(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            //database.CreateTableAsync<Stocks>().Wait();
            Console.WriteLine("Database connection achieved.");
        }


        public async Task<List<Blend>> GetBlends(DateTime date)
        {
            string sql = "SELECT * FROM blend WHERE DateOfBlend = " + "'" + date.ToString("yyyy-MM-dd") + "'";


            List<Blend> blend = await database.QueryAsync<Blend>(sql);


            return blend;
        }

        public async Task<List<CoalMovement>> GetCoalMovements(DateTime time, int minutes)
        {
            string currentTime = time.ToString("yyyy-MM-dd HH:mm:ss");
            time = time.AddMinutes(minutes);
            string futureTime = time.ToString("yyyy-MM-dd HH:mm:ss");

            currentTime = "'" + currentTime + "'";
            futureTime = "'" + futureTime + "'";


            List<CoalMovement> coalMovements = await database.QueryAsync<CoalMovement>("SELECT * FROM coalMovement " +
                                                                "where DateTimeArrival > " + currentTime +
                                                                "and DateTimeArrival <= " + futureTime);
            coalMovements.Sort();

            return coalMovements;
        }

        public async Task<List<RunOfMine>> GetRunOfMine(DateTime date)
        {
            var sql = "SELECT * FROM RunOfMine WHERE date = " + "'" + date.ToString("yyyy-MM-dd") + "'";

            var runOfMine = await database.QueryAsync<RunOfMine>(sql);

            return runOfMine;
        }
    }
}