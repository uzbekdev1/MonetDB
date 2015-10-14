using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using MonetDB.DataProvider;
using MonetDB.Driver.Data;
using MonetDB.Driver.Extensions;
using MonetDB.Enums.Logging;
using MonetDB.Extensions;
using MonetDB.Helpers;
using MonetDB.Helpers.Logging;
using MonetDB.Models;

namespace MonetDB.Driver.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class MonetDbHelper
    { 

        /// <summary>
        /// 
        /// </summary>
        public static bool TestConnected(string connectionString)
        {
            return !ExecuteScalar(connectionString, "SELECT NOW();").IsNull();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string connectionString, string commandText)
        {
            int result;

            using (var connection = new MonetDbConnection(connectionString))
            {
                connection.Open();

                using (var command = new MonetDbCommand(connection)
                {
                    CommandText = commandText
                })
                {
                    result = command.ExecuteNonQuery();
                }

                connection.Close();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandText"></param> 
        /// <returns></returns>
        public static IDataReader ExecuteReader(string connectionString, string commandText)
        {
            var connection = new MonetDbConnection(connectionString);

            connection.Open();

            var command = new MonetDbCommand(connection)
            {
                CommandText = commandText
            };

            return command.ExecuteReader();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string connectionString, string commandText)
        {
            DataSet ds;

            using (var connection = new MonetDbConnection(connectionString))
            {
                connection.Open();

                using (var command = new MonetDbCommand(connection)
                {
                    CommandText = commandText
                })
                {
                    using (var reader = command.ExecuteReader())
                    {
                        ds = reader.ToDateSet();
                    }
                }

                connection.Close();
            }

            return ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="fileName"></param>
        public static void DumpBinary(string[] rows, string fileName)
        {
            using (var file = new StreamWriter(fileName))
            {
                for (var i = 0; i < rows.Length; i++)
                {
                    if (i > 0)
                        file.Write(Environment.NewLine);

                    file.Write(rows[i]);
                }

                file.Write(Environment.NewLine);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="schemaName"></param>
        /// <param name="tableName"></param>
        /// <param name="fileNames"></param>
        /// <param name="rowCount"></param>
        public static int BinaryCopy(string connectionString, string schemaName, string tableName, int rowCount, params string[] fileNames)
        {
            var files = string.Join(",", fileNames.Select(s => $"'{s}'"));
            var query = string.IsNullOrWhiteSpace(schemaName)
               ? $"COPY {rowCount} RECORDS INTO \"{tableName}\" FROM ({files}) USING DELIMITERS '\\t', '\\n', '\"';"
                : $"COPY {rowCount} RECORDS INTO \"{schemaName}\".\"{tableName}\" FROM ({files}) USING DELIMITERS '\\t', '\\n', '\"';";

            return ExecuteNonQuery(connectionString, query);
        }
         
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string connectionString, string commandText)
        {
            object result;

            using (var connection = new MonetDbConnection(connectionString))
            {
                connection.Open();

                using (var command = new MonetDbCommand(connection)
                {
                    CommandText = commandText
                })
                {
                    result = command.ExecuteScalar();
                }

                connection.Close();
            }

            return result;
        }
    }
}
