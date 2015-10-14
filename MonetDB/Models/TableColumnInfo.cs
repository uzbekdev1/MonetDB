using System;

namespace MonetDB.Models
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class TableColumnInfo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="column"></param>
        public TableColumnInfo(string column)
        {
            Column = column;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Column { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Ordinal { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DataType { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public Type RuntimeType { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public bool Nullable { get; set; }

    }
}