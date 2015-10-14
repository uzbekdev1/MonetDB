using System;
using System.Data;
using System.Linq;
using MonetDB.Models;

namespace MonetDB.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class DbExt
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConnectionValue(this string connection, string key)
        {
            var array = connection.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            var item = array.FirstOrDefault(f => f.StartsWith(key, StringComparison.InvariantCultureIgnoreCase));

            if (item == null)
                return string.Empty;

            var itemArray = item.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);

            if (itemArray.Length != 2)
                return string.Empty;

            //connection string value 
            return itemArray[1];
        }

        /// <summary>
        /// Sets the new connection string value with specified key
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="key">Connection string key</param>
        /// <param name="value">New value</param>
        /// <returns></returns>
        public static string SetConnectionValue(this string connection, string key, string value)
        {
            var array = connection.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            var arrayIndex = 0;

            for (arrayIndex = 0; arrayIndex < array.Length; arrayIndex++)
            {
                if (array[arrayIndex].StartsWith(key, StringComparison.InvariantCultureIgnoreCase))
                {
                    array[arrayIndex] = key + "=" + value;
                    break;
                }
            }

            return string.Join(";", array);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object ExtractToObjectValue(this object value, Type type)
        {
            object obj;

            if (value == DBNull.Value)
            {
                obj = "NULL";
            }
            else
            {
                switch (Type.GetTypeCode(type))
                {
                    case TypeCode.Boolean:
                        {
                            obj = value;
                        }
                        break;
                    case TypeCode.SByte:
                    case TypeCode.Byte:
                    case TypeCode.Int16:
                    case TypeCode.UInt16:
                    case TypeCode.Int32:
                    case TypeCode.UInt32:
                    case TypeCode.Int64:
                    case TypeCode.UInt64:
                        {
                            obj = value;
                        }
                        break;
                    case TypeCode.Single:
                    case TypeCode.Double:
                    case TypeCode.Decimal:
                        {
                            obj = value;
                        }
                        break;
                    case TypeCode.DateTime:
                        {
                            obj =
                                $"TIMESTAMP WITH TIME ZONE'{Convert.ToDateTime(value).ToString("yyyy-MMMM-dd hh:mm:ss tt")}'";
                        }
                        break;
                    case TypeCode.Char:
                    case TypeCode.String:
                        {
                            obj = $"'{Convert.ToString(value).Replace("'", "''")}'";
                        }
                        break;
                    default:
                        {
                            obj = value;
                        }
                        break;
                }
            }

            return obj;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static TableColumnInfo[] GetColumns(this IDataReader reader)
        {
            if (reader == null)
                return new TableColumnInfo[] { };

            var dt = reader.GetSchemaTable();

            if (dt == null)
                return new TableColumnInfo[] { };

            var columns = new TableColumnInfo[dt.Rows.Count];

            for (var i = 0; i < columns.Length; i++)
            {
                var row = dt.Rows[i];
                var columnName = row["ColumnName"].To<string>();

                columns[i] = new TableColumnInfo(columnName)
                {
                    DataType = reader.GetDataTypeName(i),
                    Nullable = row["AllowDBNull"].ToBool(),
                    RuntimeType = Type.GetType(row["DataType"].To<string>()),
                    Ordinal = row["ColumnOrdinal"].To<int>()
                };
            }

            return columns;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static string RegisterDriverConnectionParameters(this string connectionString)
        {
            return $"database={connectionString.GetConnectionValue(MonetSettings.Connection.Database)};host={connectionString.GetConnectionValue(MonetSettings.Connection.Host)};";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="batchSize"></param>
        /// <returns></returns>
        public static object[][] GetMatrixInstance(this IDataReader reader, int batchSize)
        {
            var values = new object[batchSize][];

            for (var secondIndex = 0; secondIndex < values.Length; secondIndex++)
                values[secondIndex] = new object[reader.FieldCount];

            return values;
        }

    }
}
