using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonetDB.Models;

namespace MonetDB.Connector.Data
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseDataConnector : IDataConnector
    {
        /// <summary>
        /// 
        /// </summary>
        public BaseDataConnector()
            : this(string.Empty)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public BaseDataConnector(string connectionString)
        { 
            ConnectionString = connectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        public abstract string OpenScope { get; }

        /// <summary>
        /// 
        /// </summary>
        public abstract string CloseScope { get; }

        /// <summary>
        /// 
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract IList<SchemaTableInfo> GetSchemaTables();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="schemaName"></param>
        /// <returns></returns>
        public abstract IList<TableColumnInfo> GetTableColumns(string tableName,
            string schemaName = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual IDataReader ExecuteReader(string query, CommandType commandType = CommandType.Text)
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
