using System;
using System.Data;

namespace MonetDB.Driver.Extensions
{
    /// <summary>
    /// </summary>
    public static class TypeExt
    {
        /// <summary>
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static Type ToSystemType(this DbType dbType)
        {
            switch (dbType)
            {
                case DbType.Boolean:
                    return typeof(bool);

                case DbType.Time:
                case DbType.DateTime2:
                case DbType.Date:
                case DbType.DateTime:
                    return typeof(DateTime); 

                case DbType.Currency:
                case DbType.Decimal:
                case DbType.VarNumeric:
                case DbType.Double:
                    return typeof(float);

                case DbType.Guid:
                    return typeof(Guid);

                case DbType.UInt16:
                case DbType.Int16:
                    return typeof(short);

                case DbType.UInt32:
                case DbType.Int32:
                    return typeof(int);

                case DbType.UInt64:
                case DbType.Int64:
                    return typeof(long);

                case DbType.StringFixedLength:
                case DbType.AnsiStringFixedLength:
                case DbType.AnsiString:
                case DbType.String:
                    return typeof(string);

                default:
                    return typeof(object);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Type GetSystemType(this string value)
        {
            switch (value)
            {
                case "tinyint":
                    return typeof(byte);

                case "smallint":
                    return typeof(short);

                case "int":
                    return typeof(int);

                case "bigint":
                    return typeof(long);

                case "boolean":
                    return typeof(bool);

                case "character":
                case "char":
                    return typeof(char);

                case "varchar":
                case "text":
                case "varchar varying":
                case "string":
                    return typeof(string);

                case "double":
                case "numeric":
                case "decimal":
                case "float":
                case "real":
                    return typeof(float);

                case "daytime":
                case "time":
                case "time with time zone":
                    return typeof(TimeSpan);

                case "date":
                case "timestamp":
                case "timestamp with time zone":
                    return typeof(DateTime);

                default:
                    return typeof(object);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetDbTypeName(this Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                    return "boolean";

                case TypeCode.Char:
                    return "char";

                case TypeCode.SByte:
                case TypeCode.Byte:
                    return "tinyint";

                case TypeCode.Int16:
                case TypeCode.UInt16:
                    return "smallint";

                case TypeCode.Int32:
                case TypeCode.UInt32:
                    return "int";

                case TypeCode.Int64:
                case TypeCode.UInt64:
                    return "bigint";

                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    return "float";

                case TypeCode.DateTime:
                    return "timestamp with time zone";

                case TypeCode.String:
                    return "string";

                default:
                    return "blob";
            }
        }
    }
}