using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MonetDB.Helpers
{
    public static class BinaryFormatterHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="filename"></param>
        public static void Serialize<T>(T t, string filename) where T : class
        {
            using (var ms = File.OpenWrite(filename))
            {
                var formatter = new BinaryFormatter();

                formatter.Serialize(ms, t);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string filename) where T : class
        {
            using (var fs = File.OpenRead(filename))
            {
                var formatter = new BinaryFormatter();

                return (T)formatter.Deserialize(fs);
            }
        }
    }
}
