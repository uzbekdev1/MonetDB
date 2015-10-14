using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonetDB.DataProvider;
using MonetDB.Driver.Extensions;
using MonetDB.Driver.Helpers;
using MonetDB.Extensions;
using MonetDB.Helpers;
using MonetDB.Models;
using MonetDB.TCP.Client;
using MonetDB.TCP.Enums;
using MonetDB.TCP.Models;

namespace MonetDB.Driver.Database
{
    /// <summary>
    /// </summary>
    public sealed class MonetDatabase : IDisposable
    {
        private readonly MonetDbConnectionInfo _connectionInfo;

        /// <summary>
        /// </summary>
        /// <param name="connectionString"></param>
        public MonetDatabase(string connectionString)
        {
            _connectionInfo = new MonetDbConnectionInfo(connectionString);
        }

        public MonetDbConnectionInfo ConnectionInfo
        {
            get
            {
                var port = MonetDbtcpProxy.GetPort(_connectionInfo.Host, _connectionInfo.Database);

                _connectionInfo.SetPort(port);

                return _connectionInfo;
            }
        }

        /// <summary>
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public void DropDatabase()
        {
            MonetDbtcpProxy.DropDatabase(_connectionInfo.Host, _connectionInfo.Database);
        }

        /// <summary>
        /// 
        /// </summary>
        public void KillDatabase()
        {
            MonetDbtcpProxy.KillDatabase(_connectionInfo.Host, _connectionInfo.Database);
        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadDatabase()
        {
            MonetDbtcpProxy.LoadDatabase(_connectionInfo.Host, _connectionInfo.Database);
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public DatabaseStatus StatusDatabase()
        {
            return MonetDbtcpProxy.StatusDatabase(_connectionInfo.Host, _connectionInfo.Database);
        }

        /// <summary>
        /// </summary>
        public void CreateDatabase()
        {
            MonetDbtcpProxy.CreateDatabase(_connectionInfo.Host, _connectionInfo.Database);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<MonetDatabaseInfo> GetDatabases()
        {
            return MonetDbtcpProxy.GetDatabases(_connectionInfo.Host);
        }

        /// <summary>
        /// </summary>
        /// <param name="schemaName"></param>
        public void DropSchemaIfExists(string schemaName)
        {
            //validate schema
            var query = $@"SELECT COUNT(*) FROM SCHEMAS WHERE name ilike '{schemaName}';";
            var result = MonetDbHelper.ExecuteScalar(ConnectionInfo.ToString(), query).To<int>();

            if (result > 0)
            {
                query = $"DROP SCHEMA \"{schemaName}\";";

                MonetDbHelper.ExecuteNonQuery(ConnectionInfo.ToString(), query);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="schemaName"></param>
        /// <param name="tableName"></param>
        public void DropTableIfExists(string schemaName, string tableName)
        {
            //validate table
            var query = schemaName.IsNull()
                ? $@"SELECT COUNT(*) FROM TABLES WHERE name ilike '{tableName}';"
                : $@"SELECT COUNT(t.*) FROM TABLES AS t WHERE t.schema_id in (SELECT s.id FROM SCHEMAS AS s WHERE s.name ilike '{
                    schemaName}') AND t.name ilike '{tableName}';";
            var result = MonetDbHelper.ExecuteScalar(ConnectionInfo.ToString(), query).To<int>();

            if (result > 0)
            {
                query = schemaName.IsNull()
                    ? $"DROP TABLE \"{tableName}\";"
                    : $"DROP TABLE \"{schemaName}\".\"{tableName}\";";

                MonetDbHelper.ExecuteNonQuery(ConnectionInfo.ToString(), query);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="schemaName"></param>
        public void CreateSchemaIfNotExists(string schemaName)
        {
            //validate schema
            var query = $@"SELECT COUNT(*) FROM SCHEMAS WHERE name ilike '{schemaName}';";
            var result = MonetDbHelper.ExecuteScalar(ConnectionInfo.ToString(), query).To<int>();

            if (result == 0)
            {
                query = $"CREATE SCHEMA \"{schemaName}\";";

                MonetDbHelper.ExecuteNonQuery(ConnectionInfo.ToString(), query);
            }
        }

        /// <summary>
        ///     https://www.monetdb.org/Documentation/Manuals/SQLreference/BuiltinTypes
        /// </summary>
        /// <param name="schemaName"></param>
        /// <param name="tableName"></param>
        /// <param name="columns"></param>
        public void CreateTableIfNotExists(string schemaName, string tableName, TableColumnInfo[] columns)
        {
            //validate schema
            if (!schemaName.IsNull())
                CreateSchemaIfNotExists(schemaName);

            //validate table
            var query = schemaName.IsNull()
                ? $@"SELECT COUNT(*) FROM TABLES WHERE name ilike '{tableName}';"
                : $@"SELECT COUNT(t.*) FROM TABLES AS t WHERE t.schema_id in (SELECT s.id FROM SCHEMAS AS s WHERE s.name ilike '{
                    schemaName}') AND t.name ilike '{tableName}';";
            var result = MonetDbHelper.ExecuteScalar(ConnectionInfo.ToString(), query).To<int>();

            if (result == 0)
            {
                var sb = new StringBuilder();

                if (schemaName.IsNull())
                {
                    sb.AppendFormat("CREATE TABLE \"{0}\"(", tableName);
                }
                else
                {
                    sb.AppendFormat("CREATE TABLE \"{0}\".\"{1}\"(", schemaName, tableName);
                }

                for (var index = 0; index < columns.Count(); index++)
                {
                    var column = columns.ElementAt(index);
                    var dbType = (column.RuntimeType == null || column.DataType.GetSystemType() == column.RuntimeType)
                        ? column.DataType
                        : column.RuntimeType.GetDbTypeName();

                    if (index > 0)
                        sb.Append(",");

                    sb.AppendFormat("\"{0}\" {1} {2}", column.Column, dbType, column.Nullable.ToBool() ? "NULL" : "NOT NULL");
                }

                sb.Append(");");

                MonetDbHelper.ExecuteNonQuery(ConnectionInfo.ToString(), sb.ToString());
            }
        }


        /// <summary>
        /// </summary>
        /// <param name="schemaName"></param>
        /// <param name="tableName"></param>
        /// <param name="columns"></param>
        /// <param name="values"></param>
        /// <param name="batchSize"></param>
        public bool BulkCopy(string schemaName, string tableName, TableColumnInfo[] columns, object[][] values, int batchSize)
        {
            var statements = values.MapToFile();

            return MonetDbtcpProxy.BulkCopy(ConnectionInfo, schemaName, tableName, columns, statements, batchSize);
        }

    }
}