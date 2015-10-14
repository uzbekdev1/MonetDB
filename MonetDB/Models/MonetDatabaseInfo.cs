using System;
using MonetDB.DataProvider;
using MonetDB.Enums;
using MonetDB.TCP.Enums;

namespace MonetDB.Models
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class MonetDatabaseInfo : MonetDbConnectionInfo
    { 
        /// <summary>
        /// 
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DatabaseStatus Status { get; set; }
         
    }
}
