using System;
using System.Collections.Generic;
using System.Linq;
using MonetDB.Helpers.Win32;

namespace MonetDB.Extensions
{
    /// <summary>
    /// </summary>
    public static class TplExt
    {
        /// <summary>
        /// </summary>
        /// <param name="t"></param>
        /// <param name="parallelismCount"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        public static void ParallelInvoker<T>(this IEnumerable<T> t, int parallelismCount, Action<T> action)
        {
            if (!t.Any())
                return;

            t.AsParallel()
                .WithDegreeOfParallelism(parallelismCount)
                .ForAll(delegate(T obj)
                {
                    action(obj);
                });
        }

        /// <summary>
        /// </summary>
        /// <param name="t"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        public static void ParallelInvoker<T>(this IEnumerable<T> t, Action<T> action)
        {
            var cpuCount = ManagementObjectHelper.GetNumberOfCores();

            ParallelInvoker(t, cpuCount, action);
        }
    }
}