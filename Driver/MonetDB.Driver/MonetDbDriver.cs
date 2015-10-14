using MonetDB.Driver.BulkCopy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonetDB.Driver.Database;
using MonetDB.Driver.Helpers;
using MonetDB.Extensions;
using MonetDB.Helpers;

namespace MonetDB.Driver
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class MonetDbDriver : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public MonetDbDriver(string connectionString)
        {
            Database = new MonetDatabase(connectionString.RegisterDriverConnectionParameters());
            BulkCopy = new MonetBulkCopy(Database);
        }

        /// <summary>
        /// 
        /// </summary>
        public MonetDatabase Database { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public MonetBulkCopy BulkCopy { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
