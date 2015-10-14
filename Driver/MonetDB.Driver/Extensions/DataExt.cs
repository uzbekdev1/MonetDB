using System;
using System.Data;
using MonetDB.Extensions;

namespace MonetDB.Driver.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class DataExt
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static DataSet ToDateSet(this IDataReader reader)
        {
            var ds = new DataSet();

            do
            {

                var schema = reader.GetSchemaTable();

                if (schema == null)
                    return ds;

                var dt = new DataTable(schema.TableName);

                for (var columnIndex = 0; columnIndex < reader.FieldCount; columnIndex++)
                {
                    dt.Columns.Add(new DataColumn(schema.Rows[columnIndex][0].To<string>(), (Type)schema.Rows[columnIndex][2]));
                }

                while (reader.Read())
                {
                    var values = new object[reader.FieldCount];

                    reader.GetValues(values);

                    dt.Rows.Add(values);
                }

                ds.Tables.Add(dt);

            } while (reader.NextResult());

            return ds;
        }

    }
}
