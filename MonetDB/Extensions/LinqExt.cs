using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MonetDB.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class LinqExt
    {
        /// <summary>
        /// Returns the only element of a sequence, or a default value if the sequence is empty or contains more than one element.
        /// </summary>
        public static TSource SingleOrDefaultIfMultiple<TSource>(this IEnumerable<TSource> source)
        {
            var elements = source.Take(2).ToArray();

            return (elements.Length == 1) ? elements[0] : default(TSource);
        }

        /// <summary>
        /// Returns the only element of a sequence, or a default value if the sequence is empty or contains more than one element.
        /// </summary>
        public static TSource SingleOrDefaultIfMultiple<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return source.Where(predicate).SingleOrDefaultIfMultiple();
        }
         
    }
}
