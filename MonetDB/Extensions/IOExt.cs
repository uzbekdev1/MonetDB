using System;
using System.Linq;

namespace MonetDB.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class IoExt
    {
        /// <summary>
        /// 
        /// </summary> 
        /// <param name="values"></param>
        public static string[] MapToStdin(this object[][] values)
        {
            return values.Select(s => string.Join("\t", s.Select(a =>
            {
                //nil
                if (a.IsNull())
                {
                    return "NULL";
                }

                //date time
                if (a is DateTime)
                {
                    return $"{Convert.ToDateTime(a):s}";
                }

                //string
                if (a is string)
                {
                    return $"\"{a}\"";
                }

                //number or bool
                return a;

            }))).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string[] MapToFile(this object[][] values)
        {
            return values.Select(s => string.Join("\t", s.Select(a =>
            {
                object result;

                if (a.IsNull())
                {
                    result = "NULL";
                }
                else if (a is DateTime)
                {
                    result = Convert.ToDateTime(a).ToString("yyyy-MM-dd HH:mm:ss");
                }
                else if (a is string)
                {
                    result = $"\"{a}\"";
                }
                else
                {
                    result = a;
                }

                return result;
            }))).ToArray();
        }

    }
}
