using System;

namespace MonetDB.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class SchemaTableInfo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="table"></param>
        public SchemaTableInfo(string schema, string table)
        {
            Schema = schema;
            Table = table;
        }

        /// <summary>
        /// 
        /// </summary>
        public SchemaTableInfo(string table)
            : this(string.Empty, table)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public SchemaTableInfo()
            : this(string.Empty, string.Empty)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public string Schema { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Table { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long RowsCount { get; set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.IsNullOrWhiteSpace(Schema)
                    ? $"{Table}({RowsCount} rows)"
                : $"{Schema}.{Table}({RowsCount} rows)";
        }

    }
}