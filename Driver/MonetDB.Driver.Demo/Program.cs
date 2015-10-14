using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MonetDB.Driver.Handlers;
using MonetDB.Extensions;
using MonetDB.Models;

namespace MonetDB.Driver.Demo
{
    static class Program
    {
        private const string ConnectionString = "database=FoodMartDW;host=localhost;";

        static void Main(string[] args)
        {
            DataProviderTest.ConnectionString = ConnectionString;
            DataProviderTest.SelectTest();

            Console.ReadKey();
        }
    }
}
