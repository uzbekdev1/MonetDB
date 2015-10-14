using System;
using MonetDB.DataProvider;
using MonetDB.Models;

namespace MonetDB.TCP.Models
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class MonetBulkCopyModel : MonetDbConnectionInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string SchemaName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int BatchSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TableColumnInfo[] Columns { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] Rows { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MonetBulkCopyModel()
        {
            Rows = new byte[] { };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientModel"></param>
        public MonetBulkCopyModel(MonetDbConnectionInfo clientModel)
            : this()
        {
            Host = clientModel.Host;
            Database = clientModel.Database;

            SetPort(clientModel.GetPort());
        }
    }
}