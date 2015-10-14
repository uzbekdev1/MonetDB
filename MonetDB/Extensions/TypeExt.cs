using System;
using System.ComponentModel;

namespace MonetDB.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class TypeExt
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T To<T>(this object obj, T defaultValue = default(T))
        {
            if (obj == null || obj == DBNull.Value)
                return defaultValue;

            var tc = TypeDescriptor.GetConverter(typeof(T));

            return tc.CanConvertTo(typeof(T))
                ? (T)tc.ConvertTo(obj, typeof(T))
                : defaultValue;
        } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool IsNull(this object o)
        {
            return o == DBNull.Value ||
                   o == null ||
                   string.IsNullOrWhiteSpace(o.To<string>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object o)
        {
            DateTime dt;

            DateTime.TryParse(o.To<string>(), out dt);

            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static Guid ToGuid(this object o)
        {
            Guid uid;

            Guid.TryParse(o.To<string>(), out uid);

            return uid;
        }

    }
}
