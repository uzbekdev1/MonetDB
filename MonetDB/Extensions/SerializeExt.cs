using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MonetDB.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class SerializeExt
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string[] data)
        {
            var formatter = new BinaryFormatter();

            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, data);

                return stream.ToArray();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string[] ToStrings(this byte[] data)
        {
            var formatter = new BinaryFormatter();

            using (var stream = new MemoryStream(data))
            {
                return (string[])formatter.Deserialize(stream);
            }
        }

    }
}
