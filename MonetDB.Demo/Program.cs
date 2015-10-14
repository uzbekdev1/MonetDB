using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MonetDB.Driver;
using MonetDB.Driver.BulkCopy;
using MonetDB.Driver.Database; 
using MonetDB.Driver.Handlers;
using MonetDB.Driver.Helpers;
using MonetDB.Enums;
using MonetDB.Helpers;
using MonetDB.Models;

namespace MonetDB.Demo
{
    static class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {


#if WIN_64
            Console.WriteLine("x64");
#else
            Console.WriteLine("x86");
#endif
            Console.ReadLine();
        }
    }
}
