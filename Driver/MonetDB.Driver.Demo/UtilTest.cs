using MonetDB.TCP.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonetDB.Driver.Demo
{
    internal static class UtilTest
    {
        internal static string ConnectionString { get; set; }

        internal static void CreateDatabase()
        {
            var driver = new MonetDbDriver(ConnectionString);

            driver.Database.CreateDatabase();
        }

        internal static void DropDatabase()
        {
            var driver = new MonetDbDriver(ConnectionString);

            driver.Database.CreateDatabase();
        }

        internal static void KillDatabase()
        {
            var driver = new MonetDbDriver(ConnectionString);

            driver.Database.CreateDatabase();
        }

        internal static void LoadDatabase()
        {
            var driver = new MonetDbDriver(ConnectionString);

            driver.Database.CreateDatabase();
        }

        internal static DatabaseStatus ExistsDatabase()
        {
            var driver = new MonetDbDriver(ConnectionString);

            return driver.Database.StatusDatabase();
        }

        internal static int GetPort()
        {
            var driver = new MonetDbDriver(ConnectionString);

            return driver.Database.ConnectionInfo.GetPort();
        }
    }
}
