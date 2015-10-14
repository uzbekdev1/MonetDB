using System;
using System.Linq;

namespace MonetDB.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class StringExt
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool ToBool(this object s)
        {
            var items = new[]
            {
                "yes",
                "true",
                "1",
                "on",
                "y"
            };

            return !s.To<string>().IsNull() && items.Any(a => s.To<string>().Equals(a, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
