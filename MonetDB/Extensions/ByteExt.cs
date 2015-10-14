using Lz4Net;

namespace MonetDB.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ByteExt
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Lz4Compress(this byte[] data)
        {
            return Lz4.CompressBytes(data, Lz4Mode.HighCompression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Lz4Decompress(this byte[] data)
        {
            return Lz4.DecompressBytes(data);
        }

    }
}
