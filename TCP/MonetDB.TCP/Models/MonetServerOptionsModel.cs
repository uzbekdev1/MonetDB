using System;
using MonetDB.Helpers;

namespace MonetDB.TCP.Models
{
    /// <summary>
    /// </summary>
    [Serializable]
    public class MonetServerOptionsModel
    {
        /// <summary>
        /// </summary>
        public MonetServerOptionsModel()
        {
            Port = DnsHelper.GetAvailablePort();
            Threads = Environment.ProcessorCount;
            MaxClients = Environment.Is64BitProcess ? 64 : 32;
        }

        /// <summary>
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        ///     Multi-core usage
        ///     https://www.monetdb.org/Documentation/Guide/Multicores
        /// </summary>
        public int Threads { get; set; }

        /// <summary>
        /// </summary>
        public long MaxClients { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ServerFileName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ServerArguments { get; set; }

    }
}