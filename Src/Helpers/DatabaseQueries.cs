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

    }
}
