using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonetDB.Connector.Data;
using MonetDB.Connector.SqlServer.Helpers;
using MonetDB.Extensions;
using MonetDB.Models;

namespace MonetDB.Connector.SqlServer
{
    internal class SqlServerConnector : BaseDataConnector
    {
        public override string OpenScope => "[";

        public override string CloseScope => "]";

        public override IList<SchemaTableInfo> GetSchemaTables()
        {
            var query = @"SELECT sc.name as schema_name,
     ta.name as table_name,
     SUM(pa.rows) as row_count
 FROM sys.tables as ta
 INNER JOIN sys.partitions  as pa
 ON pa.OBJECT_ID = ta.OBJECT_ID
 INNER JOIN sys.schemas  as sc
 ON ta.schema_id = sc.schema_id
 WHERE ta.is_ms_shipped = 0 AND pa.index_id IN (1,0)
 GROUP BY sc.name,ta.name
 ORDER BY SUM(pa.rows) DESC";
            var dt = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, query);

            if (dt.Tables.Count == 0)
                return new List<SchemaTableInfo>();

            return dt.Tables[0].Rows
                .Cast<DataRow>()
                .Select(s => new SchemaTableInfo(s["schema_name"].To<string>(), s["table_name"].To<string>())
                {
                    RowsCount = s["row_count"].To<long>()
                })
                .ToList();
        }

        public override IList<TableColumnInfo> GetTableColumns(string tableName,
            string schemaName = null)
        { 
            var query =
                $@"SELECT c.*
                                        FROM INFORMATION_SCHEMA.COLUMNS AS c 
                                        WHERE  c.TABLE_SCHEMA='{
                    schemaName}' AND  c.TABLE_NAME = '{tableName}'; ";
            var ds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, query);

            if (ds.Tables.Count == 0)
                return new List<TableColumnInfo>();

            return ds.Tables[0].Rows
                .Cast<DataRow>()
                .Select(s => new TableColumnInfo(s["COLUMN_NAME"].To<string>())
                {
                    DataType = s["DATA_TYPE"].To<string>(),
                    Nullable = s["IS_NULLABLE"].ToBool()
                }).ToList();

        }

        public override IDataReader ExecuteReader(string query, CommandType commandType = CommandType.Text)
        { 
            return SqlHelper.ExecuteReader(ConnectionString, commandType, query);
        }

    }
}
