using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MonetDB.Models;

namespace MonetDB.Connector.Data
{
    /// <summary>
    /// 
    /// </summary> 
    public interface IDataConnector : IDisposable
    {

        /// <summary>
        /// 
        /// </summary>
        string OpenScope { get; }


        /// <summary>
        /// 
        /// </summary>
        string CloseScope { get; }

        /// <summary>
        /// 
        /// </summary>
        string ConnectionString { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<SchemaTableInfo> GetSchemaTables();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="schemaName"></param>
        /// <returns></returns>
        IList<TableColumnInfo> GetTableColumns(string tableName, string schemaName = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        IDataReader ExecuteReader(string query, CommandType commandType = CommandType.Text);

    }
}
